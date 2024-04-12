using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;
using Serilog;

namespace SCJ.Booking.MVC.Services
{
    public class ScBookingService
    {
        public readonly bool IsLocalDevEnvironment;

        private readonly IOnlineBooking _client;
        private readonly SessionService _session;
        private readonly IViewRenderService _viewRenderService;
        private readonly MailService _mailService;
        private readonly ScCacheService _cache;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly DbWriterService _dbWriterService;

        //Constructor
        public ScBookingService(
            ApplicationDbContext dbContext,
            IConfiguration configuration,
            SessionService sessionService,
            IViewRenderService viewRenderService,
            ScCacheService scCacheService
        )
        {
            //check if this is running on a developer workstation (outside OpenShift)
            string tagName = configuration["TAG_NAME"] ?? "";
            if (tagName.ToLower().Equals("localdev"))
            {
                IsLocalDevEnvironment = true;
            }

            _logger = LogHelper.GetLogger(configuration);
            _configuration = configuration;
            _client = OnlineBookingClientFactory.GetClient(configuration);
            _session = sessionService;
            _viewRenderService = viewRenderService;
            _cache = scCacheService;
            _mailService = new MailService("SC", configuration, _logger);
            _dbWriterService = new DbWriterService(dbContext);
        }

        /// <summary>
        ///     Populate the dropdown list for locations for the search
        /// </summary>
        public ScCaseSearchViewModel LoadSearchForm()
        {
            //clear booking info session
            _session.ScBookingInfo = null;

            //Model instance
            return new ScCaseSearchViewModel();
        }

        public ScCaseSearchViewModel ReloadSearchForm()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            return new ScCaseSearchViewModel
            {
                CaseRegistryId = bookingInfo.CaseRegistryId,
                CaseLocationName = bookingInfo.CaseLocationName,
                SelectedCaseId = bookingInfo.PhysicalFileId,
                CaseNumber = bookingInfo.CaseNumber,
                CaseSearchResults = bookingInfo.CaseSearchResults,
                SelectedCourtClass = bookingInfo.SelectedCourtClass,
                LocationPrefix = bookingInfo.LocationPrefix,
                AvailableConferenceTypeIds = bookingInfo.AvailableConferenceTypeIds
            };
        }

        public async Task<List<int>> GetAvailableConferenceTypesByLocationAsync(
            string caseLocationName
        )
        {
            var result = new List<int>();

            if (!string.IsNullOrWhiteSpace(caseLocationName))
            {
                var locations = await _cache.GetLocationsAsync();
                result = locations
                    .Where(x =>
                        x.locationName == caseLocationName
                        && (
                            x.bookingHearingTypeID == ScHearingType.TMC
                            || x.bookingHearingTypeID == ScHearingType.CPC
                            || x.bookingHearingTypeID == ScHearingType.JCC
                            || x.bookingHearingTypeID == ScHearingType.JMC
                        )
                    )
                    .Select(x => x.bookingHearingTypeID)
                    .Distinct()
                    .ToList();
            }

            return result;
        }

        // Returns booking types from the cache
        public async Task<List<string>> GetAvailableBookingTypes()
        {
            var supportedTypes = ScHearingType.HearingTypeIdMap.Keys.Select(keyName => keyName);
            return (await _cache.GetAvailableBookingTypesAsync())
                .Intersect(supportedTypes)
                .ToList();
        }

        public async Task<string> GetLocationName(int registryId)
        {
            return await _cache.GetLocationNameAsync(registryId);
        }

        public async Task<ScCaseSearchViewModel> GetSearchResults(ScCaseSearchViewModel model)
        {
            // Load locations from cache
            var newModel = new ScCaseSearchViewModel
            {
                CaseRegistryId = model.CaseRegistryId,
                CaseNumber = model.CaseNumber,
                SelectedCourtClass = model.SelectedCourtClass,
                CaseLocationName = model.CaseLocationName,
                AvailableConferenceTypeIds = model.AvailableConferenceTypeIds,
            };

            //search the current case number
            (string searchableCaseNumber, newModel.LocationPrefix) = await BuildCaseNumber(
                model.CaseNumber,
                model.CaseRegistryId
            );
            newModel.CaseSearchResults = await _client.caseNumberValidAsync(searchableCaseNumber);

            if ((newModel.CaseSearchResults?.Length ?? 0) == 0)
            {
                //get contact information
                newModel.RegistryContactNumber = GetRegistryContactNumber(model.CaseRegistryId);
            }
            else
            {
                _session.ScBookingInfo = new ScSessionBookingInfo
                {
                    CaseNumber = model.CaseNumber.GetValueOrDefault(0),
                    LocationPrefix = newModel.LocationPrefix,
                    CaseSearchResults = newModel.CaseSearchResults,
                    CaseRegistryId = model.CaseRegistryId,
                    CaseLocationName = newModel.CaseLocationName,
                };
            }

            return newModel;
        }

        public void SaveCaseSearchForm(ScCaseSearchViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            bookingInfo.PhysicalFileId = model.SelectedCaseId;
            bookingInfo.SelectedCourtClass = model.SelectedCourtClass;
            bookingInfo.FullCaseNumber = model.FullCaseNumber;
            bookingInfo.SelectedCourtClassName = model.SelectedCourtClassName;
            bookingInfo.SelectedCourtFile = model.SelectedCourtFile;
            bookingInfo.AvailableConferenceTypeIds = model.AvailableConferenceTypeIds;

            _session.ScBookingInfo = bookingInfo;
        }

        /// <summary>
        ///     Check if a time slot is still available for a court booking
        /// </summary>
        public bool IsTimeStillAvailable(AvailableDatesByLocation schedule, int containerId)
        {
            //check if the container ID is still available
            return schedule.AvailableDates.Any(x => x.ContainerID == containerId);
        }

        /// <summary>
        ///     Fetch location-code for specific case ID
        /// </summary>
        public async Task<(string, string)> BuildCaseNumber(int? caseId, int locationId)
        {
            //fetch location prefix
            string prefix = (await _cache.GetLocationAsync(locationId)).locationCode ?? "";

            //return location prefix + case number
            return ($"{prefix}{caseId}", prefix);
        }

        /// <summary>
        ///     Book court case
        /// </summary>
        public async Task<ScCaseConfirmViewModel> BookConference(
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

            // check the schedule again to make sure the time slot wasn't taken by someone else
            AvailableDatesByLocation schedule = await _client.AvailableDatesByLocationAsync(
                bookingInfo.ConferenceLocationRegistryId,
                bookingInfo.HearingTypeId
            );

            //ensure time slot is still available
            if (IsTimeStillAvailable(schedule, bookingInfo.ContainerId))
            {
                string userDisplayName = user.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
                long userId = long.Parse(user.FindFirst(ClaimTypes.Sid)?.Value ?? "0");

                //build object to send to the API
                var bookInfo = new BookHearingInfo
                {
                    CEIS_Physical_File_ID = bookingInfo.PhysicalFileId,
                    containerID = bookingInfo.ContainerId,
                    dateTime = bookingInfo.SelectedConferenceDate,
                    hearingLength = bookingInfo.ConferenceLengthMinutes,
                    locationID = bookingInfo.ConferenceLocationRegistryId,
                    requestedBy = $"{userDisplayName} {model.Phone} {model.EmailAddress}",
                    hearingTypeId = bookingInfo.HearingTypeId
                };

                _logger.Information("BOOKING SUPREME COURT => BookingHearingAsync(bookInfo)");
                _logger.Information(JsonSerializer.Serialize(bookInfo));

                //submit booking
                BookingHearingResult result = await _client.BookingHearingAsync(bookInfo);

                //get the raw result
                bookingInfo.ApiBookingResultMessage = result.bookingResult;

                //store user info in session for next booking
                var userInfo = new SessionUserInfo
                {
                    Phone = model.Phone,
                    Email = model.EmailAddress,
                    ContactName = $"{userDisplayName}"
                };
                _session.UserInfo = userInfo;

                //test to see if the booking was successful
                if (result.bookingResult.ToLower().StartsWith("success"))
                {
                    //create database entry
                    await _dbWriterService.SaveBookingHistory(
                        userId,
                        "SC",
                        bookingInfo.BookingLocationName,
                        bookingInfo.HearingTypeId
                    );

                    //update model
                    model.IsBooked = true;
                    bookingInfo.IsBooked = true;

                    var emailBody = await GetConferenceEmailBody();
                    const string emailSubject = "BC Courts Booking Confirmation";

                    //send email
                    await SendEmail(model.EmailAddress, emailSubject, emailBody);

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
            else
            {
                //The booking is not available anymore
                //user needs to choose a new time slot
                model.IsTimeSlotAvailable = false;
                model.IsBooked = false;
                bookingInfo.IsBooked = false;
            }

            // save the booking info back to the session
            _session.ScBookingInfo = bookingInfo;

            return model;
        }

        /// <summary>
        ///     Book trial
        /// </summary>
        public async Task<ScCaseConfirmViewModel> BookTrial(
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
                // @TODO: save to DB
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
                string emailBody = await GetTrialEmailBody();
                string fileNumber = bookingInfo.FullCaseNumber;
                string emailSubject = $"Trial booking request for {fileNumber}";
                await SendEmail(model.EmailAddress, emailSubject, emailBody);
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
                        HearingDate = bookingInfo.SelectedRegularTrialDate.Value
                    };
                BookingHearingResult result = await _client.BookTrialHearingAsync(requestPayload);

                //get the raw result
                bookingInfo.ApiBookingResultMessage = result.bookingResult;

                //test to see if the booking was successful
                if (result.bookingResult.ToLower().StartsWith("success"))
                {
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
                    string emailBody = await GetTrialEmailBody();
                    string fileNumber = bookingInfo.FullCaseNumber;
                    string startDate = bookingInfo.SelectedRegularTrialDate?.ToString(
                        "MMMM dd, yyyy"
                    );
                    string emailSubject = $"Trial booking for {fileNumber} starting on {startDate}";
                    await SendEmail(model.EmailAddress, emailSubject, emailBody);

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
        ///     Renders the template for the email body to a string (~/Views/ScBooking/Email.cshtml)
        /// </summary>
        private async Task<string> GetConferenceEmailBody()
        {
            //user information
            SessionUserInfo user = _session.GetUserInformation();

            //booking information
            var booking = _session.ScBookingInfo;

            //set ViewModel for the email
            var viewModel = new ScConferenceEmailViewModel
            {
                EmailAddress = user.Email,
                Phone = user.Phone,
                FullCaseNumber = booking.FullCaseNumber,
                StyleOfCause = booking.SelectedCourtFile.styleOfCause,
                CaseLocationName = booking.CaseLocationName,
                BookingLocationName = booking.BookingLocationName,
                TypeOfConference = booking.HearingTypeName,
                Date = booking.FormattedConferenceDate,
                Time = booking.FormattedConferenceTime
            };

            //Render the email template
            string template = booking.HearingTypeId switch
            {
                ScHearingType.AWS => "ScBooking/Email-CV-AWS",
                ScHearingType.JMC => "ScBooking/Email-JMC",
                ScHearingType.PTC => "ScBooking/Email-CV-PTC",
                ScHearingType.TCH => "ScBooking/Email-CV-TCH",
                ScHearingType.TMC => "ScBooking/Email-TMC",
                ScHearingType.CPC => "ScBooking/Email-CPC",
                ScHearingType.JCC => "ScBooking/Email-JCC",
                _ => throw new ArgumentException("Invalid HearingTypeId"),
            };
            return await _viewRenderService.RenderToStringAsync(template, viewModel);
        }

        /// <summary>
        ///     Renders the template for the email body to a string
        /// </summary>
        private async Task<string> GetTrialEmailBody()
        {
            var user = _session.GetUserInformation();
            var booking = _session.ScBookingInfo;

            // lottery date, when users will be notified (@TODO: confirm & handle null date?)
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
                TrialLocationName = await GetLocationName(booking.TrialLocationRegistryId),
                ResultDate = resultDate
            };

            var template =
                (booking.TrialFormulaType == ScFormulaType.FairUseBooking)
                    ? "ScBooking/Email-Trial-FairUse"
                    : "ScBooking/Email-Trial-Regular";

            // @TODO: html email body?
            return await _viewRenderService.RenderToStringAsync(template, viewModel);
        }

        /// <summary>
        ///     Get registry contact number
        /// </summary>
        public string GetRegistryContactNumber(int registryId)
        {
            //TODO:Implement logic to find contact number
            //Need to ask SCJ to add a new field to the locations API
            //for now temporary code to loop it up in a dictionary

            var numbers = new Dictionary<int, string>
            {
                { 1, "604-660-2853" },
                { 2, "250-356-1450" },
                { 3, "604-660-8551" },
                { 4, "250-614-2750" },
                { 6, "250-828-4351" },
                { 7, "250-741-5860" },
                { 9, "250-741-5860" },
                { 10, "604-795-8349" },
                { 11, "250-614-2750" },
                { 12, "250-356-1450" },
                { 13, "250-614-2750" },
                { 15, "250-828-4351" },
                { 17, "250-828-4351" },
                { 18, "250-470-6935" },
                { 20, "250-741-5860" },
                { 21, "250-828-4351" },
                { 22, "250-741-5860" },
                { 24, "250-741-5860" },
                { 25, "250-624-7474" },
                { 26, "250-470-6935" },
                { 27, "250-614-2750" },
                { 28, "250-828-4351" },
                { 29, "250-828-4351" },
                { 30, "250-828-4351" },
                { 31, "250-847-7482" },
                { 32, "250-624-7474" },
                { 33, "250-614-2750" },
                { 34, "250-470-6935" }
            };

            return numbers[registryId];
        }

        public async Task<FormulaLocation> GetFormulaLocationAsync(
            string formula,
            int locationId,
            string courtClass
        )
        {
            var formulas = await _cache.AvailableTrialBookingFormulasByLocationAsync();

            // look for a special formula location for the specific courtClass
            var result = formulas.FirstOrDefault(f =>
                f.FormulaType == formula
                && f.LocationID == locationId
                && f.BookingHearingCode == courtClass
            );

            if (result != null)
            {
                return result;
            }

            // if there isn't a special formula location then use the general one
            var all = new[] { "All", "All Other" };

            return formulas.FirstOrDefault(f =>
                f.FormulaType == formula
                && f.LocationID == locationId
                && all.Contains(f.BookingHearingCode)
            );
        }

        public async Task<Tuple<List<DateTime>, FormulaLocation>> GetAvailableTrialDatesAsync(
            string formulaType,
            FormulaLocation formula = null
        )
        {
            var bookingInfo = _session.ScBookingInfo;
            var courtClassCode = bookingInfo.SelectedCourtFile.courtClassCode ?? "";

            formula ??= await GetFormulaLocationAsync(
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

        public async Task SaveBookingTypeFormAsync(ScBookingTypeViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            bookingInfo.AvailableConferenceTypeIds = model.AvailableConferenceTypeIds;

            //set hearing type name
            bookingInfo.HearingTypeId = model.HearingTypeId;
            bookingInfo.HearingTypeName = ScHearingType.HearingTypeNameMap[model.HearingTypeId];

            bookingInfo.ConferenceLocationRegistryId =
                await _cache.GetConferenceBookingLocationIdAsync(
                    bookingInfo.CaseRegistryId,
                    bookingInfo.HearingTypeId
                ) ?? bookingInfo.CaseRegistryId;

            bookingInfo.BookingLocationName = await _cache.GetLocationNameAsync(
                model.HearingTypeId == ScHearingType.TRIAL
                    ? model.TrialLocationRegistryId
                    : bookingInfo.ConferenceLocationRegistryId
            );

            bookingInfo.AvailableConferenceDates = await _client.AvailableDatesByLocationAsync(
                bookingInfo.ConferenceLocationRegistryId,
                bookingInfo.HearingTypeId
            );

            bookingInfo.EstimatedTrialLength = model.EstimatedTrialLength;
            bookingInfo.IsHomeRegistry = model.IsHomeRegistry;
            bookingInfo.IsLocationChangeFiled = model.IsLocationChangeFiled;

            // set trial location:
            if (model.IsHomeRegistry == true)
            {
                // home registry
                bookingInfo.TrialLocationRegistryId = bookingInfo.CaseRegistryId;
            }
            else if (model.IsHomeRegistry == false && model.IsLocationChangeFiled == true)
            {
                // somewhere besides the home registry
                bookingInfo.TrialLocationRegistryId = model.TrialLocationRegistryId;
            }

            _session.ScBookingInfo = bookingInfo;
        }

        public ScBookingTypeViewModel LoadBookingTypeForm()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            return new ScBookingTypeViewModel
            {
                HearingTypeId = bookingInfo.HearingTypeId,
                HearingTypeName = bookingInfo.HearingTypeName,
                EstimatedTrialLength = bookingInfo.EstimatedTrialLength,
                IsHomeRegistry = bookingInfo.IsHomeRegistry,
                IsLocationChangeFiled = bookingInfo.IsLocationChangeFiled,
                TrialLocationRegistryId = bookingInfo.TrialLocationRegistryId,
                AvailableConferenceTypeIds = bookingInfo.AvailableConferenceTypeIds,
                SessionInfo = bookingInfo
            };
        }

        public async Task SaveAvailableTimesForm(ScAvailableTimesViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            // check the schedule again to make sure the time slot wasn't taken by someone else
            AvailableDatesByLocation schedule = await _client.AvailableDatesByLocationAsync(
                bookingInfo.ConferenceLocationRegistryId,
                bookingInfo.HearingTypeId
            );

            if (model.ContainerId > 0)
            {
                bookingInfo.SelectedConferenceDateTicks = model.SelectedConferenceDate;
                model.TimeSlotExpired = !IsTimeStillAvailable(schedule, model.ContainerId);
                bookingInfo.ContainerId = model.ContainerId;
            }

            bookingInfo.SelectedConferenceDate = model.ParsedConferenceDate;
            bookingInfo.SelectedRegularTrialDate = model.SelectedRegularTrialDate;
            bookingInfo.SelectedFairUseTrialDates = model.SelectedFairUseTrialDates;
            bookingInfo.TrialFormulaType = model.TrialFormulaType;

            _session.ScBookingInfo = bookingInfo;
        }

        public async Task<ScAvailableTimesViewModel> LoadAvailableTimesForm()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            var model = new ScAvailableTimesViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                HearingTypeId = bookingInfo.HearingTypeId,
                AvailableConferenceDates = bookingInfo.AvailableConferenceDates,
                ConferenceLocationRegistryId = bookingInfo.ConferenceLocationRegistryId,
                SelectedRegularTrialDate = bookingInfo.SelectedRegularTrialDate,
                SelectedFairUseTrialDates = bookingInfo.SelectedFairUseTrialDates,
                SessionInfo = bookingInfo
            };

            model = await SetFairUseFormulaInfo(model);

            model.TrialFormulaType = bookingInfo.FairUseFormula is null
                ? ScFormulaType.RegularBooking
                : bookingInfo.TrialFormulaType;

            return model;
        }

        public async Task<ScAvailableTimesViewModel> SetFairUseFormulaInfo(
            ScAvailableTimesViewModel model,
            FormulaLocation fairUseFormula = null
        )
        {
            var bookingInfo = _session.ScBookingInfo;

            fairUseFormula ??= await GetFormulaLocationAsync(
                ScFormulaType.FairUseBooking,
                bookingInfo.TrialLocationRegistryId,
                bookingInfo.SelectedCourtFile.courtClassCode
            );

            model.FairUseStartDate = fairUseFormula?.FairUseBookingPeriodStartDate;
            model.FairUseEndDate = fairUseFormula?.FairUseBookingPeriodEndDate;
            model.FairUseResultDate = fairUseFormula?.FairUseContactDate;
            model.FairUseNoticeDate = fairUseFormula?.FairUseBookingPeriodEndDate;

            return model;
        }

        private async Task SendEmail(string toEmail, string subject, string body)
        {
            if (!IsLocalDevEnvironment)
            {
                await _mailService.ExchangeSendEmail(toEmail, subject, body);
            }
            else
            {
                var fromEmail = _configuration["FROM_EMAIL"];
                await _mailService.SendGridSendEmail(fromEmail, toEmail, subject, body);
            }
        }
    }
}
