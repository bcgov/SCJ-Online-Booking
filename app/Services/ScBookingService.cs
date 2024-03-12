using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;
using Serilog;

namespace SCJ.Booking.MVC.Services
{
    public class ScBookingService
    {
        // todo: this is usually 10.  It was changed to int.MaxValue on 4/20/2020 to quickly disable the limit.
        public const int MaxHearingsPerDay = int.MaxValue;
        public readonly bool IsLocalDevEnvironment;

        private const string EmailSubject = "BC Courts Booking Confirmation";
        private readonly IOnlineBooking _client;
        private readonly ApplicationDbContext _dbContext;
        private readonly HttpContext _httpContext;
        private readonly SessionService _session;
        private readonly IViewRenderService _viewRenderService;
        private readonly MailService _mailService;
        private readonly ScCacheService _cache;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        //Constructor
        public ScBookingService(
            ApplicationDbContext dbContext,
            IHttpContextAccessor httpAccessor,
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
            _dbContext = dbContext;
            _httpContext = httpAccessor.HttpContext;
            _session = sessionService;
            _viewRenderService = viewRenderService;
            _cache = scCacheService;
            _mailService = new MailService("SC", configuration, _logger);
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

        public ScCaseSearchViewModel LoadSearchForm2()
        {
            var bookingInfo = _session.ScBookingInfo;

            // Previously-selected "regular booking" trial date
            string trialDate =
                bookingInfo.FullDate.ToString("yyyy") == "0001"
                    ? ""
                    : bookingInfo.FullDate.ToString("yyyy-MM-dd");

            // @TODO: trialDates list for "fair use" booking

            //Model instance
            return new ScCaseSearchViewModel
            {
                CaseRegistryId = bookingInfo.CaseRegistryId,
                CaseLocationName = bookingInfo.CaseLocationName,
                SelectedCaseId = bookingInfo.CaseId,
                CaseNumber = bookingInfo.CaseNumber,
                CourtFiles = bookingInfo.CourtFiles,
                SelectedCourtClass = bookingInfo.SelectedCourtClass,
                FullCaseNumber = bookingInfo.FullCaseNumber,
                LocationPrefix = bookingInfo.LocationPrefix,
                HearingTypeId = bookingInfo.HearingTypeId,
                HearingTypeName = bookingInfo.HearingTypeName,
                EstimatedTrialLength = bookingInfo.EstimatedTrialLength,
                IsHomeRegistry = bookingInfo.IsHomeRegistry,
                IsLocationChangeFiled = bookingInfo.IsLocationChangeFiled,
                TrialLocation = bookingInfo.TrialLocation,
                Results = bookingInfo.Results,
                BookingLocationName = bookingInfo.BookingLocationName,
                BookingRegistryId = bookingInfo.BookingRegistryId,
                AvailableConferenceTypeIds = bookingInfo.AvailableConferenceTypeIds,
                BookingFormula = bookingInfo.BookingFormula,
                SelectedTrialDate = trialDate,
            };
        }

        public async Task<List<int>> GetConferenceTypesAsync(string caseLocationName)
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
                        )
                    )
                    .Select(x => x.bookingHearingTypeID)
                    .Distinct()
                    .ToList();
            }

            return result;
        }

        /// <summary>
        ///     Search for available times
        /// </summary>
        public async Task<ScCaseSearchViewModel> GetSearchResults(ScCaseSearchViewModel model)
        {
            // Load locations from cache
            var retval = new ScCaseSearchViewModel
            {
                HearingTypeId = model.HearingTypeId,
                CaseRegistryId = model.CaseRegistryId,
                CaseNumber = model.CaseNumber,
                TimeSlotExpired = model.TimeSlotExpired,
                SelectedCourtClass = model.SelectedCourtClass
            };

            //set hearing type name
            if (
                retval.HearingTypeId > 0
                && ScHearingType.HearingTypeNameMap.ContainsKey(retval.HearingTypeId)
            )
            {
                retval.HearingTypeName = ScHearingType.HearingTypeNameMap[retval.HearingTypeId];
            }

            //set selected registry name
            retval.CaseLocationName = await _cache.GetLocationNameAsync(retval.CaseRegistryId);

            // set booking location information
            retval.BookingRegistryId =
                await _cache.GetBookingLocationIdAsync(retval.CaseRegistryId, retval.HearingTypeId)
                ?? retval.CaseRegistryId;

            retval.BookingLocationName = await _cache.GetLocationNameAsync(
                retval.BookingRegistryId
            );

            //search the current case number
            (retval.FullCaseNumber, retval.LocationPrefix) = await BuildCaseNumber(
                model.CaseNumber,
                model.CaseRegistryId
            );
            retval.CourtFiles = await _client.caseNumberValidAsync(retval.FullCaseNumber);

            if (!retval.IsValidCaseNumber)
            {
                //get contact information
                retval.RegistryContactNumber = GetRegistryContactNumber(model.CaseRegistryId);
            }
            else
            {
                retval.Results = await _client.AvailableDatesByLocationAsync(
                    retval.BookingRegistryId,
                    model.HearingTypeId
                );

                //check for valid date
                if (model.ContainerId > 0)
                {
                    retval.TimeSlotExpired = !IsTimeStillAvailable(
                        retval.Results,
                        model.ContainerId
                    );

                    //convert JS ticks to .Net date
                    DateTime? dt = new DateTime(Convert.ToInt64(model.SelectedCaseDate));

                    //set date properties
                    retval.ContainerId = model.ContainerId;
                    retval.SelectedCaseDate = model.SelectedCaseDate;

                    string bookingTime =
                        $"{dt.Value:hh:mm tt} to {dt.Value.AddMinutes(retval.HearingLengthMinutes):hh:mm tt}";

                    retval.TimeSlotFriendlyName = $"{dt.Value:MMMM dd} from {bookingTime}";
                }

                _session.ScBookingInfo = new ScSessionBookingInfo
                {
                    ContainerId = model.ContainerId,
                    CaseNumber = model.CaseNumber.ToUpper().Trim(),
                    FullCaseNumber = retval.FullCaseNumber,
                    CaseId = (int)retval.CourtFiles[0].physicalFileId,
                    HearingTypeId = model.HearingTypeId,
                    HearingTypeName = retval.HearingTypeName,
                    Results = retval.Results,
                    CaseRegistryId = model.CaseRegistryId,
                    CaseLocationName = retval.CaseLocationName,
                    BookingRegistryId = retval.BookingRegistryId,
                    BookingLocationName = retval.BookingLocationName,
                    SelectedCaseDate = model.SelectedCaseDate,
                };
            }

            return retval;
        }

        public async Task<List<int>> GetConferenceTypeIds(ScCaseSearchViewModel model)
        {
            //set selected registry name
            model.CaseLocationName = await _cache.GetLocationNameAsync(model.CaseRegistryId);

            return await GetConferenceTypesAsync(model.CaseLocationName);
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

        public async Task<ScCaseSearchViewModel> GetSearchResults2(ScCaseSearchViewModel model)
        {
            // Load locations from cache
            var result = new ScCaseSearchViewModel
            {
                CaseRegistryId = model.CaseRegistryId,
                CaseNumber = model.CaseNumber,
                SelectedCourtClass = model.SelectedCourtClass,
                CaseLocationName = model.CaseLocationName,
                AvailableConferenceTypeIds = model.AvailableConferenceTypeIds,
            };

            //search the current case number
            (result.FullCaseNumber, result.LocationPrefix) = await BuildCaseNumber(
                model.CaseNumber,
                model.CaseRegistryId
            );
            result.CourtFiles = await _client.caseNumberValidAsync(result.FullCaseNumber);

            if (!result.IsValidCaseNumber)
            {
                //get contact information
                result.RegistryContactNumber = GetRegistryContactNumber(model.CaseRegistryId);
            }
            else
            {
                _session.ScBookingInfo = new ScSessionBookingInfo
                {
                    CaseNumber = model.CaseNumber.ToUpper().Trim(),
                    FullCaseNumber = result.FullCaseNumber,
                    LocationPrefix = result.LocationPrefix,
                    CourtFiles = result.CourtFiles,
                    CaseRegistryId = model.CaseRegistryId,
                    CaseLocationName = result.CaseLocationName,
                };
            }

            return result;
        }

        public async Task SaveScBookingInfoAsync(ScCaseSearchViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.CaseId != model.SelectedCaseId)
            {
                bookingInfo.CaseId = model.SelectedCaseId;
            }

            if (
                !string.IsNullOrWhiteSpace(model.SelectedCourtClass)
                && bookingInfo.SelectedCourtClass != model.SelectedCourtClass
            )
            {
                bookingInfo.SelectedCourtClass = model.SelectedCourtClass;
            }

            if (
                !string.IsNullOrWhiteSpace(model.FileNumber)
                && bookingInfo.FileNumber != model.FileNumber
            )
            {
                bookingInfo.FileNumber = model.FileNumber;
            }

            if (
                model.CourtFiles != null
                && bookingInfo.SelectedCourtClassName != model.SelectedCourtClassName
            )
            {
                bookingInfo.SelectedCourtClassName = model.SelectedCourtClassName;
            }

            bookingInfo.SelectedCourtFile = model.SelectedCourtFile;
            bookingInfo.AvailableConferenceTypeIds = model.AvailableConferenceTypeIds;

            //set hearing type name
            if (
                model.HearingTypeId > 0
                && ScHearingType.HearingTypeNameMap.ContainsKey(model.HearingTypeId)
                && bookingInfo.HearingTypeId != model.HearingTypeId
            )
            {
                bookingInfo.HearingTypeId = model.HearingTypeId;
                bookingInfo.HearingTypeName = ScHearingType.HearingTypeNameMap[model.HearingTypeId];

                bookingInfo.BookingRegistryId =
                    await _cache.GetBookingLocationIdAsync(
                        bookingInfo.CaseRegistryId,
                        bookingInfo.HearingTypeId
                    ) ?? bookingInfo.CaseRegistryId;

                bookingInfo.BookingLocationName = await _cache.GetLocationNameAsync(
                    bookingInfo.BookingRegistryId
                );

                bookingInfo.Results = await _client.AvailableDatesByLocationAsync(
                    bookingInfo.BookingRegistryId,
                    bookingInfo.HearingTypeId
                );
            }

            if (
                model.EstimatedTrialLength.HasValue
                && bookingInfo.EstimatedTrialLength != model.EstimatedTrialLength.Value
            )
            {
                bookingInfo.EstimatedTrialLength = model.EstimatedTrialLength.Value;
            }

            if (
                model.IsHomeRegistry.HasValue
                && bookingInfo.IsHomeRegistry != model.IsHomeRegistry.Value
            )
            {
                bookingInfo.IsHomeRegistry = model.IsHomeRegistry.Value;
            }

            if (
                model.IsHomeRegistry == false
                && model.IsLocationChangeFiled.HasValue
                && bookingInfo.IsLocationChangeFiled != model.IsLocationChangeFiled.Value
            )
            {
                bookingInfo.IsLocationChangeFiled = model.IsLocationChangeFiled.Value;
            }

            // set trial location:
            if (
                model.IsHomeRegistry == true
                && bookingInfo.CaseRegistryId > 0
                && bookingInfo.TrialLocation != bookingInfo.CaseRegistryId
            )
            {
                // home registry
                bookingInfo.TrialLocation = bookingInfo.CaseRegistryId;
            }
            else if (
                model.IsHomeRegistry == false
                && model.IsLocationChangeFiled == true
                && model.TrialLocation > 0
                && bookingInfo.TrialLocation != model.TrialLocation
            )
            {
                // somewhere besides the home registry
                bookingInfo.TrialLocation = model.TrialLocation;
            }

            if (model.ContainerId > 0)
            {
                if (
                    !string.IsNullOrWhiteSpace(model.SelectedCaseDate)
                    && bookingInfo.SelectedCaseDate != model.SelectedCaseDate
                )
                {
                    bookingInfo.SelectedCaseDate = model.SelectedCaseDate;
                }

                model.TimeSlotExpired = !IsTimeStillAvailable(
                    bookingInfo.Results,
                    model.ContainerId
                );

                if (bookingInfo.ContainerId != model.ContainerId)
                {
                    bookingInfo.ContainerId = model.ContainerId;
                }
            }

            if (bookingInfo.FullDate != model.FullDate)
            {
                bookingInfo.FullDate = model.FullDate;
            }

            if (bookingInfo.BookingFormula != model.BookingFormula)
            {
                bookingInfo.BookingFormula = model.BookingFormula;
            }

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
        public async Task<(string, string)> BuildCaseNumber(string caseId, int locationId)
        {
            //fetch location prefix
            string prefix = (await _cache.GetLocationAsync(locationId)).locationCode ?? "";

            //return location prefix + case number
            return ($"{prefix}{caseId}", prefix);
        }

        /// <summary>
        ///     Book court case
        /// </summary>
        public async Task<ScCaseConfirmViewModel> BookCourtCase(
            ScCaseConfirmViewModel model,
            string userGuid,
            string userDisplayName
        )
        {
            //if the user could not be detected return
            if (string.IsNullOrWhiteSpace(userGuid))
            {
                return model;
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            // check the schedule again to make sure the time slot wasn't taken by someone else
            AvailableDatesByLocation schedule = await _client.AvailableDatesByLocationAsync(
                bookingInfo.BookingRegistryId,
                bookingInfo.HearingTypeId
            );

            //ensure time slot is still available
            if (IsTimeStillAvailable(schedule, bookingInfo.ContainerId))
            {
                //build object to send to the API
                var bookInfo = new BookHearingInfo
                {
                    CEIS_Physical_File_ID = bookingInfo.CaseId,
                    containerID = bookingInfo.ContainerId,
                    dateTime = model.FullDate,
                    hearingLength = bookingInfo.HearingLengthMinutes,
                    locationID = bookingInfo.BookingRegistryId,
                    requestedBy = $"{userDisplayName} {model.Phone} {model.EmailAddress}",
                    hearingTypeId = bookingInfo.HearingTypeId
                };

                _logger.Information("BOOKING SUPREME COURT => BookingHearingAsync(bookInfo)");
                _logger.Information(JsonSerializer.Serialize(bookInfo));

                //submit booking
                BookingHearingResult result = await _client.BookingHearingAsync(bookInfo);

                //get the raw result
                bookingInfo.RawResult = result.bookingResult;

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
                    DbSet<BookingHistory> bookingHistory = _dbContext.Set<BookingHistory>();

                    bookingHistory.Add(
                        new BookingHistory
                        {
                            ContainerId = bookingInfo.ContainerId,
                            SmGovUserGuid = userGuid,
                            Timestamp = DateTime.UtcNow
                        }
                    );

                    //save to DB
                    await _dbContext.SaveChangesAsync();

                    //update model
                    model.IsBooked = true;
                    bookingInfo.IsBooked = true;

                    var emailBody = await GetEmailBody();

                    //send email
                    if (!IsLocalDevEnvironment)
                    {
                        await _mailService.ExchangeSendEmail(
                            model.EmailAddress,
                            EmailSubject,
                            emailBody
                        );
                    }
                    else
                    {
                        var fromEmail = _configuration["FROM_EMAIL"];
                        await _mailService.SendGridSendEmail(
                            fromEmail,
                            model.EmailAddress,
                            EmailSubject,
                            emailBody
                        );
                    }

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
            string userGuid,
            string userDisplayName
        )
        {
            //if the user could not be detected return
            if (string.IsNullOrWhiteSpace(userGuid))
            {
                return model;
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            // @TODO: check if timeslot is available? else return error

            // @TODO: book hearing? trial?

            // @TODO: store user info in session for next booking

            // @TODO: save to DB

            // update model
            model.IsBooked = true;
            bookingInfo.IsBooked = true;

            // @TODO: send email

            // save the booking info back to the session
            _session.ScBookingInfo = bookingInfo;

            return model;
        }

        /// <summary>
        ///     Get the number of hearings left for the day
        /// </summary>
        public HtmlString GetHearingsRemaining()
        {
            return new HtmlString("");

            // todo: 4/20/2020 - I commented this out to quickly disable the message in the header. MaxHearingsPerDay was also changed from 10 to int.MaxValue.
            // int hearingsRemaining = GetUserHearingsTotalRemaining();
            //
            // switch (hearingsRemaining)
            // {
            //     case MaxHearingsPerDay:
            //         return new HtmlString($"You can book {MaxHearingsPerDay} hearings today.");
            //     case 1:
            //         return new HtmlString("You can book 1 more hearing today.");
            //     case 0:
            //         return new HtmlString("You cannot book anymore hearings today.");
            //     default:
            //         return new HtmlString($"You can book {hearingsRemaining} more hearings today.");
            // }
        }

        /// <summary>
        ///     Read the database and get the total number of hearings left for the day
        /// </summary>
        public int GetUserHearingsTotalRemaining()
        {
            //get user GUID
            string userGuid;

            if (!IsLocalDevEnvironment)
            {
                //try and read the header
                if (_httpContext.Request.Headers.ContainsKey("smgov_userguid"))
                {
                    userGuid = _httpContext.Request.Headers["smgov_userguid"].ToString();
                }
                else
                {
                    return MaxHearingsPerDay;
                }
            }
            else
            {
                //Dummy user guid
                userGuid = "B8C1EC79-6464-4C62-BF33-05FC00CC21A0";
            }

            //today's date
            var today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            //get all entries for logged-in user
            //booked on today
            List<BookingHistory> hearingsBookedForToday = _dbContext
                .BookingHistory.Where(b =>
                    b.SmGovUserGuid == userGuid
                    && b.Timestamp.Day == today.Day
                    && b.Timestamp.Month == today.Month
                    && b.Timestamp.Year == today.Year
                )
                .ToList();

            return MaxHearingsPerDay - hearingsBookedForToday.Count;
        }

        /// <summary>
        ///     Renders the template for the email body to a string (~/Views/ScBooking/Email.cshtml)
        /// </summary>
        private async Task<string> GetEmailBody()
        {
            //user information
            SessionUserInfo user = _session.GetUserInformation();

            //booking information
            var booking = _session.ScBookingInfo;

            //set ViewModel for the email
            var viewModel = new EmailViewModel
            {
                EmailAddress = user.Email,
                Phone = user.Phone,
                CourtFileNumber = booking.FileNumber,
                StyleOfCause = booking.SelectedCourtFile.styleOfCause,
                CaseLocationName = booking.CaseLocationName,
                BookingLocationName = booking.BookingLocationName,
                TypeOfConference = booking.HearingTypeName,
                Date = booking.DateFriendlyName,
                Time = booking.TimeSlotFriendlyName
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

        public async Task<List<DateTime>> GetAvailableTrialDatesAsync(string formulaType)
        {
            var bookingInfo = _session.ScBookingInfo;
            var courtClassCode = bookingInfo.SelectedCourtFile.courtClassCode;

            var formula = await GetFormulaLocationAsync(
                formulaType,
                bookingInfo.TrialLocation,
                courtClassCode
            );

            if (formula == null)
            {
                return new List<DateTime>();
            }

            AvailableTrialDatesRequestInfo trialDatesRequestInfo =
                new AvailableTrialDatesRequestInfo
                {
                    LocationID = bookingInfo.TrialLocation,
                    BookingLocationID = formula.BookingLocationID,
                    Courtclass = courtClassCode,
                    FormulaType = formulaType,
                    StartDate = formula.StartDate, // dynamic? 18 months from now?
                    EndDate = formula.EndDate,
                    HearingLength = bookingInfo.EstimatedTrialLength ?? 1, // @TODO: this shouldn't be null
                };

            AvailableTrialDatesResult availableDates =
                await _client.AvailableTrialDatesByLocationAsync(trialDatesRequestInfo);

            return availableDates
                .AvailableTrialDates.AvailablesDatesInfo.Select(d => d.AvailableDate)
                .ToList();
        }
    }
}
