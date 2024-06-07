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
    public class LotteryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IOnlineBooking _client;
        private readonly MailQueueService _mailQueueService;

        public LotteryService(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = LogHelper.GetLogger(configuration);
            _client = OnlineBookingClientFactory.GetClient(configuration);
            _mailQueueService = new MailQueueService(configuration, dbContext);
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
                FairUseBookingPeriodStartDate = toProcess.FairUseBookingPeriodStartDate,
                FairUseBookingPeriodEndDate = toProcess.FairUseBookingPeriodEndDate,
                InitiationTime = DateTime.Now,
            };

            // group the lottery entries into batches (mini lotteries) by trial booking location and
            // court class groupings
            var lotteryBatch = await GetLotteryBatch(
                toProcess.BookingLocationId,
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
        private async Task<ScTrialBookingRequest?> GetNextLotteryEntry(ScLottery lotteryInProgress)
        {
            return await _dbContext
                .ScTrialBookingRequests.Include(x => x.TrialDateSelections)
                .Where(x => x.Lottery == lotteryInProgress && x.IsProcessed == false)
                .OrderByDescending(x => x.FairUseSort)
                .ThenBy(x => x.LotteryPosition)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Gets the first booking request that is ready to process, ordered
        ///     by trial booking location and then by court class formula grouping.
        ///     This is just used to trigger the start of a lottery, prior to the
        ///     ranking of entries.
        /// </summary>
        private async Task<ScTrialBookingRequest?> CheckRequestsReadyToProcess()
        {
            return await _dbContext
                .ScTrialBookingRequests.Where(x =>
                    x.Lottery == null
                    && x.LotteryStartDate < LocalTimeWithoutZone
                    && x.IsProcessed == false
                )
                .OrderBy(x => x.BookingLocationId)
                .ThenBy(x => x.BookHearingCode)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///    Gets a batch of lottery entries based on trial booking location
        ///    and court class formula grouping
        /// </summary>
        private async Task<List<ScTrialBookingRequest>> GetLotteryBatch(
            int bookingLocationId,
            string bookHearingCode
        )
        {
            return await _dbContext
                .ScTrialBookingRequests.Where(x =>
                    x.Lottery == null
                    && x.BookHearingCode == bookHearingCode
                    && x.BookingLocationId == bookingLocationId
                    && x.LotteryStartDate < LocalTimeWithoutZone
                    && x.IsProcessed == false
                )
                .ToListAsync();
        }

        /// <summary>
        ///     Tries to book the date selections for a single lottery entry, and
        ///     then records an unmet demand for the first selection if booking is
        ///     unsuccessful
        /// </summary>
        private async Task ProcessSingleEntry(ScTrialBookingRequest entry)
        {
            bool trialBooked = false;
            var orderedSelections = entry.TrialDateSelections.OrderBy(d => d.Rank).ToArray();

            foreach (var selection in orderedSelections)
            {
                BookTrialHearingInfo scssTrialRequest =
                    new()
                    {
                        BookingLocationID = entry.BookingLocationId,
                        CEIS_Physical_File_ID = entry.CeisPhysicalFileId,
                        CourtClass = entry.CourtClassCode,
                        FormulaType = ScFormulaType.FairUseBooking,
                        HearingLength = entry.HearingLength,
                        HearingType = ScHearingType.TRIAL,
                        LocationID = entry.TrialLocationId,
                        RequestedBy = $"{entry.RequestedByName} {entry.Phone} {entry.Email}",
                        HearingDate = selection.TrialStartDate,
                        SCJOB_Trial_Booking_ID = entry.TrialBookingId,
                        SCJOB_Trial_Booking_Date = DateTime.Now
                    };

                _logger.Debug("BookTrialHearingAsync()");
                _logger.Debug(JsonSerializer.Serialize(scssTrialRequest));

                // try to book the selected date in SCSS
                BookingHearingResult result = await _client.BookTrialHearingAsync(scssTrialRequest);

                _logger.Debug(JsonSerializer.Serialize(result));

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
                    await QueueSuccessEmail(entry);
                    break;
                }
                else
                {
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
                await QueueFailureEmail(entry);
            }
        }

        /// <summary>
        ///   Records an unmet demand (hearing type 20538) for the user's first lottery selection
        /// </summary>
        private async Task RecordUnmetDemand(ScTrialBookingRequest entry)
        {
            if (entry.TrialDateSelections.Any())
            {
                var firstSelection = entry.TrialDateSelections.OrderBy(d => d.Rank).ToArray()[0];

                BookTrialHearingInfo unmetDemandRequest =
                    new()
                    {
                        BookingLocationID = entry.BookingLocationId,
                        CEIS_Physical_File_ID = entry.CeisPhysicalFileId,
                        CourtClass = entry.CourtClassCode,
                        FormulaType = ScFormulaType.FairUseBooking,
                        HearingLength = entry.HearingLength,
                        HearingType = ScHearingType.UNMET_DEMAND,
                        LocationID = entry.TrialLocationId,
                        RequestedBy = $"{entry.RequestedByName} {entry.Phone} {entry.Email}",
                        HearingDate = firstSelection.TrialStartDate,
                        SCJOB_Trial_Booking_ID = entry.TrialBookingId,
                        SCJOB_Trial_Booking_Date = DateTime.Now
                    };

                _logger.Debug("Record unmet demand");
                _logger.Debug(JsonSerializer.Serialize(unmetDemandRequest));

                BookingHearingResult result = await _client.BookTrialHearingAsync(
                    unmetDemandRequest
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
            _logger.Information($"BookHearingCode={lotteryInProgress.BookHearingCode}");
        }

        /// <summary>
        ///     Generates a booking success email and adds it to the mail queue
        /// </summary>
        private async Task QueueSuccessEmail(ScTrialBookingRequest entry)
        {
            var model = new LotteryEmailViewModel(entry);
            string emailText = await RazorHelper.RenderTemplate("Lottery-Success.cshtml", model);
            string subject =
                $"Trial booking for {model.FullCaseNumber} starting on {model.FairUseDate}";

            await _mailQueueService.QueueEmailAsync(
                "SC",
                model.EmailAddress,
                subject,
                emailText,
                isLotteryResult: true
            );
        }

        /// <summary>
        ///     Generates an unsuccessful booking email and adds it to the mail queue
        /// </summary>
        private async Task QueueFailureEmail(ScTrialBookingRequest entry)
        {
            var model = new LotteryEmailViewModel(entry);
            string emailText = await RazorHelper.RenderTemplate("Lottery-Failure.cshtml", model);
            string subject = $"No trial booking for {model.FullCaseNumber}";

            await _mailQueueService.QueueEmailAsync(
                "SC",
                model.EmailAddress,
                subject,
                emailText,
                isLotteryResult: true
            );
        }

        /// <summary>
        ///     Subtracts the current local timezone offset from UTC
        ///     e.g. -7 hours for PDT or -8 hours for PST
        ///     Postgres stores timestamps without timezone information
        ///     We only use this for date comparisons in db queries.
        /// </summary>
        private DateTime LocalTimeWithoutZone =>
            DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }
}
