using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;
using SCJ.Booking.RemoteAPIs;
using SCJ.Booking.TaskRunner.Services;
using SCJ.Booking.TaskRunner.Utils;
using SCJ.OnlineBooking;
using Serilog;

namespace SCJ.Booking.MVC.Services.SC
{
    public class ScLotteryEnabledBookingService
    {
        public readonly bool IsLocalDevEnvironment;

        private readonly IOnlineBooking _client;
        private readonly ILogger _logger;
        private readonly SessionService _session;
        private readonly DataWriterService _dbWriterService;
        private readonly IViewRenderService _viewRenderService;
        private readonly MailQueueService _mailService;
        private readonly ScCacheService _cache;
        private readonly ApplicationDbContext _dbContext;

        //Constructor
        public ScLotteryEnabledBookingService(
            ApplicationDbContext dbContext,
            IConfiguration configuration,
            SessionService sessionService,
            IViewRenderService viewRenderService,
            ScCacheService cacheService
        )
        {
            //check if this is running on a developer workstation (outside OpenShift)
            string tagName = configuration["TAG_NAME"] ?? "";
            if (tagName.ToLower().Equals("localdev"))
            {
                IsLocalDevEnvironment = true;
            }

            _logger = LogHelper.GetLogger(configuration);
            _client = OnlineBookingClientFactory.GetClient(configuration);
            _dbContext = dbContext;
            _session = sessionService;
            _viewRenderService = viewRenderService;
            _mailService = new MailQueueService(configuration, dbContext);
            _dbWriterService = new DataWriterService(dbContext);
            _cache = cacheService;
        }

        /// <summary>
        ///     Loads the available times form with session info
        /// </summary>
        public async Task<ScAvailableTimesViewModel> LoadAvailableTimesFormAsync()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            var model = new ScAvailableTimesViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                HearingTypeId = bookingInfo.HearingTypeId,
                AvailableConferenceDates = bookingInfo.AvailableConferenceDates,
                ConferenceLocationRegistryId = bookingInfo.BookingLocationRegistryId,
                SelectedRegularDate = bookingInfo.SelectedRegularDate,
                SelectedFairUseDates = bookingInfo.SelectedFairUseDates,
                SessionInfo = bookingInfo
            };

            model = await LoadAvailableTimesFormulaInfoAsync(model, null);

            model.FormulaType =
                bookingInfo.FairUseFormula is null && bookingInfo.RegularFormula is not null
                    ? ScFormulaType.RegularBooking
                    : bookingInfo.FormulaType;

            return model;
        }

        /// <summary>
        ///    Loads the available times form with formula info
        /// </summary>
        public async Task<ScAvailableTimesViewModel> LoadAvailableTimesFormulaInfoAsync(
            ScAvailableTimesViewModel model,
            FormulaLocation fairUseFormula
        )
        {
            var bookingInfo = _session.ScBookingInfo;

            fairUseFormula ??= await _cache.GetFormulaLocationAsync(
                ScFormulaType.FairUseBooking,
                bookingInfo.AlternateLocationRegistryId,
                bookingInfo.SelectedCourtFile?.courtClassCode ?? "",
                bookingInfo.HearingTypeId
            );

            // The fair use start/end dates are the period inwhich dates are selected for the lottery
            model.FairUseStartDate = fairUseFormula?.FairUseBookingPeriodStartDate;
            model.FairUseEndDate = fairUseFormula?.FairUseBookingPeriodEndDate;

            // The fair use "result date" is the date when the lottery takes place and users are notified
            model.FairUseResultDate = fairUseFormula?.FairUseContactDate;

            // The fair use "selection date" is the period inwhich the trials booked by
            // the lottery take place. Example: "June 2025" for trials in June 2025
            model.FairUseSelectionDate = fairUseFormula?.StartDate;

            return model;
        }

        /// <summary>
        ///    Saves the available times form to session
        /// </summary>
        public void SaveAvailableTimesFormAsync(ScAvailableTimesViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            bookingInfo.SelectedRegularDate = model.SelectedRegularDate;
            bookingInfo.SelectedFairUseDates = model
                .SelectedFairUseDates.Take(ScGeneral.ScMaxTrialDateSelections)
                .ToList();
            bookingInfo.FormulaType = model.FormulaType;

            _session.ScBookingInfo = bookingInfo;
        }

        /// <summary>
        ///     Book trial
        /// </summary>
        public async Task<ScCaseConfirmViewModel> BookTrialAsync(
            ScCaseConfirmViewModel model,
            ClaimsPrincipal user
        )
        {
            //if the user could not be detected return
            if (user == null)
            {
                return model;
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            model.IsBooked = false;
            bookingInfo.IsBooked = false;

            string userDisplayName = OpenIdConnectHelper.GetUserFullName(user);
            long userId = long.Parse(user.FindFirst(ClaimTypes.Sid)?.Value ?? "0");

            // store user info in session for next booking
            var userInfo = new SessionUserInfo
            {
                Phone = model.Phone,
                Email = model.EmailAddress,
                ContactName = $"{userDisplayName}"
            };
            _session.UserInfo = userInfo;

            // generate a trial booking id (for troubleshooting between SCSS and SCJOB)
            var lotteryEntryId = GenerateTrialBookingId() + "-" + userId;

            if (bookingInfo.FormulaType == ScFormulaType.FairUseBooking)
            {
                if (await CheckIfTrialAlreadyRequestedAsync())
                {
                    bookingInfo.ApiBookingResultMessage =
                        "A trial has already been requested for this case.";
                }
                else
                {
                    await _dbWriterService.SaveBookingHistory(
                        userId,
                        "SC",
                        bookingInfo.BookingLocationName,
                        ScHearingType.TRIAL,
                        ScFormulaType.FairUseBooking
                    );

                    bookingInfo.LotteryEntryId = lotteryEntryId;
                    _session.ScBookingInfo = bookingInfo;
                    await _dbWriterService.SaveFairUseRequest(userId, bookingInfo, userInfo);

                    //update model
                    model.IsBooked = true;
                    bookingInfo.IsBooked = true;

                    // send email
                    string emailBody = await GetTrialEmailBodyAsync();
                    string fileNumber = bookingInfo.FullCaseNumber;
                    string emailSubject = $"Trial booking request for {fileNumber}";
                    await _mailService.QueueEmailAsync(
                        "SC",
                        model.EmailAddress,
                        emailSubject,
                        emailBody
                    );
                }
            }
            else if (bookingInfo.FormulaType == ScFormulaType.RegularBooking)
            {
                // Available dates
                (List<DateTime> availableTrialDates, _) = await GetAvailableTrialDatesAsync(
                    ScFormulaType.RegularBooking,
                    bookingInfo.RegularFormula
                );

                // check if selected date exists in the available dates
                bool dateAvailable =
                    bookingInfo.SelectedRegularDate.HasValue
                    && availableTrialDates.Contains(bookingInfo.SelectedRegularDate.Value);

                // throw an exception if the date is no longer available
                if (!dateAvailable)
                {
                    throw new InvalidOperationException(
                        "The date you selected is no longer available."
                    );
                }

                // book trial in API
                BookTrialHearingInfo requestPayload =
                    new()
                    {
                        BookingLocationID = bookingInfo.RegularFormula.BookingLocationID,
                        CEIS_Physical_File_ID = bookingInfo.PhysicalFileId,
                        CourtClass = bookingInfo.SelectedCourtFile.courtClassCode,
                        FormulaType = ScFormulaType.RegularBooking,
                        HearingLength = bookingInfo.EstimatedTrialLength.GetValueOrDefault(1),
                        HearingType = bookingInfo.HearingTypeId,
                        LocationID = bookingInfo.AlternateLocationRegistryId,
                        RequestedBy = $"{userDisplayName} {model.Phone} {model.EmailAddress}",
                        HearingDate = bookingInfo.SelectedRegularDate.Value,
                        SCJOB_Trial_Booking_ID = lotteryEntryId,
                        SCJOB_Trial_Booking_Date = DateTime.Now
                    };

                _logger.Information("BOOKING SUPREME COURT => BookTrialHearingAsync()");
                _logger.Information(JsonSerializer.Serialize(requestPayload));

                BookingHearingResult result = await _client.scTrialBookHearingAsync(requestPayload);

                //get the raw result
                bookingInfo.ApiBookingResultMessage = result.bookingResult;

                //test to see if the booking was successful
                if (result.bookingResult.ToLower().StartsWith("success"))
                {
                    bookingInfo.LotteryEntryId = lotteryEntryId;
                    _session.ScBookingInfo = bookingInfo;

                    //create database entry
                    await _dbWriterService.SaveBookingHistory(
                        userId,
                        "SC",
                        bookingInfo.BookingLocationName,
                        ScHearingType.TRIAL,
                        ScFormulaType.RegularBooking
                    );

                    // update model
                    model.IsBooked = true;
                    bookingInfo.IsBooked = true;

                    // send email
                    string emailBody = await GetTrialEmailBodyAsync();
                    string fileNumber = bookingInfo.FullCaseNumber;
                    string startDate = bookingInfo.SelectedRegularDate?.ToString("MMMM d, yyyy");
                    string emailSubject = $"Trial booking for {fileNumber} starting on {startDate}";
                    await _mailService.QueueEmailAsync(
                        "SC",
                        model.EmailAddress,
                        emailSubject,
                        emailBody
                    );

                    //clear booking info session
                    _session.ScBookingInfo = null;
                }
                else
                {
                    _logger.Information($"API Response: {result.bookingResult}");
                    model.IsBooked = false;
                    bookingInfo.IsBooked = false;
                }
            }

            // save the booking info back to the session
            _session.ScBookingInfo = bookingInfo;

            return model;
        }

        /// <summary>
        ///     Renders the template for the email body to a string
        /// </summary>
        private async Task<string> GetTrialEmailBodyAsync()
        {
            var user = _session.GetUserInformation();
            var booking = _session.ScBookingInfo;

            // lottery date, when users will be notified
            string resultDate =
                booking.FairUseFormula?.FairUseContactDate?.ToString("dddd MMMM d, yyyy")
                ?? "[N/A]";

            // set ViewModel for the email
            var viewModel = new ScTrialEmailViewModel(booking)
            {
                EmailAddress = user.Email,
                Phone = user.Phone,
                LocationPrefix = booking.LocationPrefix,
                FullCaseNumber = booking.FullCaseNumber,
                StyleOfCause = booking.SelectedCourtFile.styleOfCause,
                CourtClassName = booking.SelectedCourtClassName,
                CaseLocationName = booking.CaseLocationName,
                BookingLocationName = booking.BookingLocationName,
                TrialLocationName = await _cache.GetLocationNameAsync(
                    booking.AlternateLocationRegistryId
                ),
                ResultDate = resultDate,
                LotteryEntryId = booking.LotteryEntryId
            };

            var template =
                booking.FormulaType == ScFormulaType.FairUseBooking
                    ? "ScCore/Emails/Email-Trial-FairUse"
                    : "ScCore/Emails/Email-Trial-Regular";

            // @TODO: html email body?
            return await _viewRenderService.RenderToStringAsync(template, viewModel);
        }

        public async Task<Tuple<List<DateTime>, FormulaLocation>> GetAvailableTrialDatesAsync(
            string formulaType,
            FormulaLocation formula
        )
        {
            var bookingInfo = _session.ScBookingInfo;
            var courtClassCode = bookingInfo.SelectedCourtFile.courtClassCode ?? "";

            formula ??= await _cache.GetFormulaLocationAsync(
                formulaType,
                bookingInfo.AlternateLocationRegistryId,
                courtClassCode,
                bookingInfo.HearingTypeId
            );

            if (formula == null)
            {
                return Tuple.Create(new List<DateTime>(), (FormulaLocation)null);
            }

            AvailableTrialDatesRequestInfo trialDatesRequestInfo =
                new()
                {
                    LocationID = bookingInfo.AlternateLocationRegistryId,
                    BookingLocationID = formula.BookingLocationID,
                    Courtclass = courtClassCode,
                    FormulaType = formulaType,
                    StartDate = formula.StartDate,
                    EndDate = formula.EndDate,
                    HearingLength = bookingInfo.EstimatedTrialLength.GetValueOrDefault(1),
                    HearingTypeId = bookingInfo.HearingTypeId
                };

            AvailableTrialDatesResult availableDates =
                await _client.scAvailableDatesByHearingTypeAndLocationAsync(trialDatesRequestInfo);

            if (availableDates.AvailableTrialDates.AvailablesDatesInfo == null)
            {
                return Tuple.Create(new List<DateTime>(), formula);
            }

            var dates = availableDates
                .AvailableTrialDates.AvailablesDatesInfo.Select(d => d.AvailableDate)
                .OrderBy(date => date)
                .ToList();

            return Tuple.Create(dates, formula);
        }

        public string GenerateTrialBookingId()
        {
            return DateTime.Now.ToString("yyMMddHHmmss");
        }

        public async Task<bool> CheckIfTrialAlreadyRequestedAsync()
        {
            var booking = _session.ScBookingInfo;

            // skip this check if we are already going to show the other message
            if (booking.SelectedCourtFile.futureTrialHearing)
            {
                return false;
            }

            return await _dbContext.ScLotteryBookingRequests.AnyAsync(r =>
                r.CaseNumber == booking.CaseNumber // has index
                && r.CaseRegistryId == booking.CaseRegistryId
                && r.CourtClassCode == booking.SelectedCourtFile.courtClassCode
                && r.IsProcessed == false
            );
        }
    }
}
