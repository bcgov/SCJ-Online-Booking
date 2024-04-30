using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Utils;
using SCJ.Booking.MVC.Constants;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;
using SCJ.Booking.RemoteAPIs;
using SCJ.Booking.TaskRunner.Services;
using SCJ.OnlineBooking;
using Serilog;

namespace SCJ.Booking.MVC.Services.SC
{
    public class ScTrialBookingService
    {
        public readonly bool IsLocalDevEnvironment;

        private readonly IOnlineBooking _client;
        private readonly ILogger _logger;
        private readonly SessionService _session;
        private readonly DataWriterService _dbWriterService;
        private readonly IViewRenderService _viewRenderService;
        private readonly MailQueueService _mailService;
        private readonly ScCacheService _cache;

        //Constructor
        public ScTrialBookingService(
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
            _session = sessionService;
            _viewRenderService = viewRenderService;
            _mailService = new MailQueueService(dbContext);
            _dbWriterService = new DataWriterService(dbContext);
            _cache = cacheService;
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

            string userDisplayName = user.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
            long userId = long.Parse(user.FindFirst(ClaimTypes.Sid)?.Value ?? "0");

            // store user info in session for next booking
            var userInfo = new SessionUserInfo
            {
                Phone = model.Phone,
                Email = model.EmailAddress,
                ContactName = $"{userDisplayName}"
            };
            _session.UserInfo = userInfo;

            if (bookingInfo.TrialFormulaType == ScFormulaType.FairUseBooking)
            {
                await _dbWriterService.SaveBookingHistory(
                    userId,
                    "SC",
                    bookingInfo.BookingLocationName,
                    ScHearingType.TRIAL,
                    ScFormulaType.FairUseBooking
                );

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
            else if (bookingInfo.TrialFormulaType == ScFormulaType.RegularBooking)
            {
                // Available dates
                (List<DateTime> availableTrialDates, _) = await GetAvailableTrialDatesAsync(
                    ScFormulaType.RegularBooking,
                    bookingInfo.RegularFormula
                );

                // check if selected date exists in the available dates
                bool dateAvailable =
                    bookingInfo.SelectedRegularTrialDate.HasValue
                    && availableTrialDates.Contains(bookingInfo.SelectedRegularTrialDate.Value);

                // throw an exception if the date is no longer available
                if (!dateAvailable)
                {
                    throw new InvalidOperationException(
                        "The date you selected is no longer available."
                    );
                }
                var trialBookingId = GenerateTrialBookingId() + "-" + userId;

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
                        LocationID = bookingInfo.TrialLocationRegistryId,
                        RequestedBy = $"{userDisplayName} {model.Phone} {model.EmailAddress}",
                        HearingDate = bookingInfo.SelectedRegularTrialDate.Value,
                        SCJOB_Trial_Booking_ID = trialBookingId,
                        SCJOB_Trial_Booking_Date = DateTime.Now
                    };

                _logger.Information("BOOKING SUPREME COURT => BookTrialHearingAsync()");
                _logger.Information(JsonSerializer.Serialize(requestPayload));

                BookingHearingResult result = await _client.BookTrialHearingAsync(requestPayload);

                //get the raw result
                bookingInfo.ApiBookingResultMessage = result.bookingResult;

                //test to see if the booking was successful
                if (result.bookingResult.ToLower().StartsWith("success"))
                {
                    bookingInfo.TrialBookingId = trialBookingId;
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
                    string startDate = bookingInfo.SelectedRegularTrialDate?.ToString(
                        "MMMM dd, yyyy"
                    );
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
                booking.FairUseFormula.FairUseContactDate?.ToString("dddd, MMMM dd, yyyy")
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
                    booking.TrialLocationRegistryId
                ),
                ResultDate = resultDate,
                TrialBookingId = booking.TrialBookingId
            };

            var template =
                booking.TrialFormulaType == ScFormulaType.FairUseBooking
                    ? "ScBooking/Emails/Email-Trial-FairUse"
                    : "ScBooking/Emails/Email-Trial-Regular";

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
                bookingInfo.TrialLocationRegistryId,
                courtClassCode
            );

            if (formula == null)
            {
                return Tuple.Create(new List<DateTime>(), (FormulaLocation)null);
            }

            AvailableTrialDatesRequestInfo trialDatesRequestInfo =
                new()
                {
                    LocationID = bookingInfo.TrialLocationRegistryId,
                    BookingLocationID = formula.BookingLocationID,
                    Courtclass = courtClassCode,
                    FormulaType = formulaType,
                    StartDate = formula.StartDate,
                    EndDate = formula.EndDate,
                    HearingLength = bookingInfo.EstimatedTrialLength.GetValueOrDefault(1)
                };

            AvailableTrialDatesResult availableDates =
                await _client.AvailableTrialDatesByLocationAsync(trialDatesRequestInfo);

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
            return DateTime.Now.ToString("yyMMddHHmm");
        }
    }
}
