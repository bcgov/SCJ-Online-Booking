using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.Data.Models;
using SCJ.Booking.RemoteAPIs;
using SCJ.Booking.TaskRunner.Utils;
using SCJ.OnlineBooking;
using Serilog;

namespace SCJ.Booking.TaskRunner.Services
{
    /// <summary>
    /// This exception is thrown to indicate a fatal error during the first booking attempt,
    /// which will cancel the lottery process.
    /// </summary>
    public class FatalBookingFailureException : Exception
    {
        public FatalBookingFailureException(string message)
            : base(message) { }
    }

    public class LotteryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IOnlineBooking _client;
        private readonly MailQueueService _mailQueueService;

        // track the results of the first booking attempt
        private bool _isFirstAttempt;
        private readonly bool _usingFakeApi;

        public LotteryService(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = LogHelper.GetLogger(configuration);
            _client = OnlineBookingClientFactory.GetClient(configuration);
            _mailQueueService = new MailQueueService(configuration, dbContext);
            _isFirstAttempt = true;
            _usingFakeApi = configuration["USE_FAKE_API"] == "true";
        }

        /// <summary>
        ///    Each time this is called, one of four things will happen
        ///    1. A new lottery is initiated for a single formula at a single location
        ///    2. A single lottery entry is processed
        ///    3. The lottery already in progress is marked as complete
        ///    4. Nothing
        /// </summary>
        /// <returns>
        ///    Boolean indicating if there is incomplete work left to do
        /// </returns>
        public async Task<bool> RunNextLotteryStep()
        {
            try
            {
                var lotteryInProgress = await _dbContext.ScLotteries.FirstOrDefaultAsync(x =>
                    x.CompletionTime == null
                );

                if (lotteryInProgress == null)
                {
                    return await StartLottery();
                }
                else
                {
                    var nextEntry = await GetNextLotteryEntry(lotteryInProgress);

                    if (nextEntry != null)
                    {
                        await ProcessSingleEntry(nextEntry);
                        return true;
                    }
                    else
                    {
                        await FinishLottery(lotteryInProgress);
                        return await CheckRequestsReadyToProcess() != null;
                    }
                }
            }
            catch (FatalBookingFailureException ex)
            {
                _logger.Error($"Fatal error: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> StartLottery()
        {
            var toProcess = await CheckRequestsReadyToProcess();

            if (toProcess == null)
            {
                return false;
            }

            var newLottery = new ScLottery
            {
                BookHearingCode = toProcess.BookHearingCode,
                BookingLocationId = toProcess.BookingLocationId,
                HearingTypeId = toProcess.HearingTypeId,
                FairUseBookingPeriodStartDate = toProcess.FairUseBookingPeriodStartDate,
                FairUseBookingPeriodEndDate = toProcess.FairUseBookingPeriodEndDate,
                InitiationTime = DateTime.Now,
            };

            // group the lottery entries into batches (mini lotteries) by booking location and
            // court class groupings
            var lotteryBatch = await GetLotteryBatch(
                toProcess.BookingLocationId,
                toProcess.HearingTypeId,
                toProcess.BookHearingCode
            );

            // randomize the list order
            lotteryBatch.RandomizeListOrder();

            // attach the entries to the new lottery and assign the random lottery positions
            for (var index = 0; index < lotteryBatch.Count; index++)
            {
                lotteryBatch[index].Lottery = newLottery;
                lotteryBatch[index].LotteryPosition = index + 1;
            }

            await _dbContext.SaveChangesAsync();

            _logger.Information("Lottery started!");
            _logger.Information($"BookingLocationId={newLottery.BookingLocationId}");
            _logger.Information($"HearingTypeId={newLottery.HearingTypeId}");
            _logger.Information($"BookHearingCode={newLottery.BookHearingCode}");
            _logger.Information(
                $"FairUseBookingPeriodEndDate={newLottery.FairUseBookingPeriodEndDate}"
            );
            _logger.Information($"EntryCount={lotteryBatch.Count}");

            return true;
        }

        /// <summary>
        ///     Gets the next entry to process in the current running lottery
        /// </summary>
        private async Task<ScLotteryBookingRequest?> GetNextLotteryEntry(
            ScLottery lotteryInProgress
        )
        {
            return await _dbContext
                .ScLotteryBookingRequests.Include(x => x.DateSelections)
                .Where(x => x.Lottery == lotteryInProgress && x.IsProcessed == false)
                .OrderByDescending(x => x.FairUseSort)
                .ThenBy(x => x.LotteryPosition)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Gets the first booking request that is ready to process, ordered
        ///     by booking location and then by court class formula grouping.
        ///     This is just used to trigger the start of a lottery, prior to the
        ///     ranking of entries.
        /// </summary>
        private async Task<ScLotteryBookingRequest?> CheckRequestsReadyToProcess()
        {
            return await _dbContext
                .ScLotteryBookingRequests.Where(x =>
                    x.Lottery == null && x.LotteryStartDate < DateTime.Now && x.IsProcessed == false
                )
                .OrderBy(x => x.BookingLocationId)
                .ThenBy(x => x.HearingTypeId)
                .ThenBy(x => x.BookHearingCode)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///    Gets a batch of lottery entries based on booking location
        ///    and court class formula grouping
        /// </summary>
        private async Task<List<ScLotteryBookingRequest>> GetLotteryBatch(
            int bookingLocationId,
            int hearingTypeId,
            string bookHearingCode
        )
        {
            return await _dbContext
                .ScLotteryBookingRequests.Where(x =>
                    x.Lottery == null
                    && x.HearingTypeId == hearingTypeId
                    && x.BookHearingCode == bookHearingCode
                    && x.BookingLocationId == bookingLocationId
                    && x.LotteryStartDate < DateTime.Now
                    && x.IsProcessed == false
                )
                .ToListAsync();
        }

        /// <summary>
        ///     Tries to book the date selections for a single lottery entry, and
        ///     then records an unmet demand for the first selection if booking is
        ///     unsuccessful
        /// </summary>
        private async Task ProcessSingleEntry(ScLotteryBookingRequest entry)
        {
            bool trialBooked = false;
            var orderedSelections = entry.DateSelections.OrderBy(d => d.Rank).ToArray();

            foreach (var selection in orderedSelections)
            {
                BookingHearingResult result = await TryBookingHearing(entry, selection);

                if (result.bookingResult.ToLower().StartsWith("success"))
                {
                    // successfully booked the selection
                    trialBooked = true;
                    _logger.Information(
                        $"Successfully booked selection {selection.Rank} for {entry.CeisPhysicalFileId}"
                    );
                    entry.AllocatedSelectionRank = selection.Rank;
                    selection.BookingResult = new string(result.bookingResult.Truncate(255));
                    entry.ProcessingTimestamp = DateTime.Now;
                    entry.IsProcessed = true;
                    await _dbContext.SaveChangesAsync();
                    if (entry.HearingTypeId == ScHearingType.LONG_CHAMBERS)
                    {
                        await _mailQueueService.QueueLongChambersSuccessEmail(entry);
                    }
                    else
                    {
                        await _mailQueueService.QueueTrialSuccessEmail(entry);
                    }
                    _isFirstAttempt = false;
                    break;
                }
                else
                {
                    // If the first booking attempt of the lottery process failed, and we're not
                    // using the fake API, throw a fatal exception to terminate the lottery due to
                    // a possible SCSS issue. We do this to prevent sending 5000 failing requests
                    // to SCSS.
                    if (_isFirstAttempt && !_usingFakeApi)
                    {
                        throw new FatalBookingFailureException(
                            "The first booking attempt of the monthly lottery process failed.\n"
                                + $"CEIS_Physical_File_ID: {entry.CeisPhysicalFileId}.\n"
                                + $"Booking result: {result.bookingResult}\n"
                                + "Terminating the lottery due to possible SCSS issue. The OpenShift pod should restart itself."
                        );
                    }

                    // record the failure
                    selection.BookingResult = new string(result.bookingResult.Truncate(255));
                }
            }

            if (!trialBooked)
            {
                await RecordUnmetDemand(entry);
                entry.ProcessingTimestamp = DateTime.Now;
                entry.IsProcessed = true;
                await _dbContext.SaveChangesAsync();
                if (entry.HearingTypeId == ScHearingType.LONG_CHAMBERS)
                {
                    await _mailQueueService.QueueLongChambersFailureEmail(entry);
                }
                else
                {
                    await _mailQueueService.QueueTrialFailureEmail(entry);
                }
            }
        }

        /// <summary>
        ///   Records an unmet demand (hearing type 20538) for the user's first lottery selection
        /// </summary>
        private async Task RecordUnmetDemand(ScLotteryBookingRequest entry)
        {
            _logger.Debug("Record unmet demand");
            if (entry.DateSelections.Any())
            {
                var firstSelection = entry.DateSelections.OrderBy(d => d.Rank).ToArray()[0];

                BookingHearingResult result = await TryBookingHearing(
                    entry,
                    firstSelection,
                    isUnmetDemand: true
                );

                if (result.bookingResult.ToLower().StartsWith("success"))
                {
                    entry.UnmetDemandBookingResult = "Success - Unmet Demand Recorded";
                    _logger.Information(
                        $"Successfully recorded unmet demand for {entry.CeisPhysicalFileId}"
                    );
                }
                else
                {
                    entry.UnmetDemandBookingResult = new string(result.bookingResult.Truncate(255));
                    _logger.Error($"Failed to record unmet demand for {entry.CeisPhysicalFileId}");
                    _logger.Error(JsonSerializer.Serialize(result));
                }
            }
            else
            {
                entry.UnmetDemandBookingResult = "Fail - No date selections found";
            }
        }

        /// <summary>
        ///     Marks a lottery as complete
        /// </summary>
        private async Task FinishLottery(ScLottery lotteryInProgress)
        {
            // finish the lottery
            lotteryInProgress.CompletionTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            _logger.Information("Lottery finished!");
            _logger.Information($"BookingLocationId={lotteryInProgress.BookingLocationId}");
            _logger.Information($"HearingTypeId={lotteryInProgress.HearingTypeId}");
            _logger.Information($"BookHearingCode={lotteryInProgress.BookHearingCode}");
        }

        /// <summary>
        ///    Tries to book a hearing for a lottery date selection
        /// </summary>
        private async Task<BookingHearingResult> TryBookingHearing(
            ScLotteryBookingRequest bookingRequest,
            ScLotteryDateSelection dateSelection,
            bool isUnmetDemand = false
        )
        {
            BookingHearingResult result;
            if (bookingRequest.HearingTypeId == ScHearingType.TRIAL)
            {
                int hearingType = isUnmetDemand ? ScHearingType.UNMET_DEMAND : ScHearingType.TRIAL;

                BookTrialHearingInfo scssTrialRequest =
                    new()
                    {
                        BookingLocationID = bookingRequest.BookingLocationId,
                        CEIS_Physical_File_ID = bookingRequest.CeisPhysicalFileId,
                        CourtClass = bookingRequest.CourtClassCode,
                        FormulaType = ScFormulaType.FairUseBooking,
                        HearingLength = bookingRequest.HearingLength,
                        HearingType = hearingType,
                        LocationID = bookingRequest.LocationId,
                        RequestedBy =
                            $"{bookingRequest.RequestedByName} {bookingRequest.Phone} {bookingRequest.Email}",
                        HearingDate = dateSelection.StartDate,
                        SCJOB_Trial_Booking_ID = bookingRequest.LotteryEntryId,
                        SCJOB_Trial_Booking_Date = DateTime.Now
                    };

                _logger.Debug("scTrialBookHearingAsync()");
                _logger.Debug(JsonSerializer.Serialize(scssTrialRequest));

                // try to book the selected date in SCSS
                result = await _client.scTrialBookHearingAsync(scssTrialRequest);

                _logger.Debug(JsonSerializer.Serialize(result));
            }
            else
            {
                int hearingType = isUnmetDemand
                    ? ScHearingType.UNMET_DEMAND
                    : bookingRequest.LongChambersHearingSubTypeId.GetValueOrDefault(
                        ScHearingType.LONG_CHAMBERS
                    );

                BookingSCCHHearingInfo scssChambersRequest =
                    new()
                    {
                        BookingLocationID = bookingRequest.BookingLocationId,
                        CEIS_Physical_File_ID = bookingRequest.CeisPhysicalFileId,
                        CourtClass = bookingRequest.CourtClassCode,
                        FormulaType = ScFormulaType.FairUseBooking,
                        HearingLength = bookingRequest.HearingLength,
                        HearingTypeId = hearingType,
                        LocationID = bookingRequest.LocationId,
                        RequestedBy =
                            $"{bookingRequest.RequestedByName} {bookingRequest.Phone} {bookingRequest.Email}",
                        HearingDate = dateSelection.StartDate
                    };

                _logger.Debug("scCHBookHearingAsync()");
                _logger.Debug(JsonSerializer.Serialize(scssChambersRequest));

                // try to book the selected date in SCSS
                result = await _client.scCHBookHearingAsync(scssChambersRequest);

                _logger.Debug(JsonSerializer.Serialize(result));
            }
            return result;
        }
    }
}
