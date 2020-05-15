using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.MVC.Data;
using SCJ.Booking.MVC.Models;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Task = System.Threading.Tasks.Task;

namespace SCJ.Booking.MVC.Services
{
    public class ScBookingService
    {
        // todo: this is usually 10.  It was changed to int.MaxValue on 4/20/2020 to quickly disable the limit.
        public const int MaxHearingsPerDay = int.MaxValue; 

        private const string EmailSubject = "BC Courts Booking Confirmation";
        private readonly IOnlineBooking _client;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        private readonly HttpContext _httpContext;
        private readonly Logger _logger;
        private readonly SessionService _session;
        private readonly IViewRenderService _viewRenderService;
        public readonly bool IsLocalDevEnvironment;
        private readonly MailService _mailService;

        //Constructor
        public ScBookingService(ApplicationDbContext dbContext, IHttpContextAccessor httpAccessor,
            IConfiguration configuration, SessionService sessionService,
            IViewRenderService viewRenderService)
        {
            // default log level is error (less verbose)
            var logLevel = LogEventLevel.Error;

            //check if this is running on a developer workstation (outside OpenShift)
            string tagName = configuration["TAG_NAME"] ?? "";
            if (tagName.ToLower().Equals("localdev"))
            {
                IsLocalDevEnvironment = true;
                logLevel = LogEventLevel.Debug;
            }

            // check if this is the OpenShift development environment
            if (tagName.ToLower().Equals("dev"))
            {
                logLevel = LogEventLevel.Information;
            }

            //setup error logger settings
            _logger = new LoggerConfiguration()
                .WriteTo.Console(logLevel)
                .CreateLogger();

            _client = OnlineBookingClientFactory.GetClient(configuration);
            _configuration = configuration;
            _dbContext = dbContext;
            _httpContext = httpAccessor.HttpContext;
            _session = sessionService;
            _viewRenderService = viewRenderService;
            _mailService = new MailService("SC", _configuration, _logger);
        }

        /// <summary>
        ///     Populate the dropdown list for locations for the search
        /// </summary>
        public async Task<ScCaseSearchViewModel> LoadSearchForm()
        {
            //Load locations from API
            Location[] locations = await _client.getLocationsAsync();

            //clear booking info session
            _session.ScBookingInfo = null;

            //Model instance
            return new ScCaseSearchViewModel
            {
                RegistryOptions = new SelectList(
                    locations.Select(x => new {Id = x.locationID, Value = x.locationName})
                        .Distinct(),
                    "Id", "Value")
            };
        }


        /// <summary>
        ///     Search for available times
        /// </summary>
        public async Task<ScCaseSearchViewModel> GetSearchResults(ScCaseSearchViewModel model)
        {
            // Load locations from API
            Location[] locations = await _client.getLocationsAsync();

            var retval = new ScCaseSearchViewModel
            {
                RegistryOptions = new SelectList(
                    locations.Select(x => new { Id = x.locationID, Value = x.locationName })
                        .Distinct(),
                    "Id", "Value"),
                HearingTypeId = model.HearingTypeId,
                SelectedRegistryId = model.SelectedRegistryId,
                CaseNumber = model.CaseNumber,
                TimeSlotExpired = model.TimeSlotExpired,
                SelectedCourtClass = model.SelectedCourtClass
            };


            //set hearing type name
            if (retval.HearingTypeId > 0 && ScHearingType.HearingTypeNameMap.ContainsKey(retval.HearingTypeId))
            {
                retval.HearingTypeName = ScHearingType.HearingTypeNameMap[retval.HearingTypeId];
            }

            //set location name
            SelectListItem selectedRegistry =
                retval.RegistryOptions.FirstOrDefault(x =>
                    x.Value == retval.SelectedRegistryId.ToString());

            if (selectedRegistry != null)
            {
                retval.SelectedRegistryName = selectedRegistry.Text;
            }

            //search the current case number
            string caseNumber = await BuildCaseNumber(model.CaseNumber, model.SelectedRegistryId);
            int caseId = await _client.caseNumberValidAsync(caseNumber);

            if (caseId == 0)
            {
                //case could not be found
                retval.IsValidCaseNumber = false;

                //empty result set
                retval.Results = new AvailableDatesByLocation();

                //get contact infromation
                retval.RegistryContactNumber = GetRegistryContactNumber(model.SelectedRegistryId);
            }
            else
            {
                //valid case number
                retval.IsValidCaseNumber = true;

                AvailableDatesByLocation schedule =
                    await _client.AvailableDatesByLocationAsync(model.SelectedRegistryId,
                        model.HearingTypeId);

                int hearingLength = schedule.BookingDetails.detailBookingLength;

                retval.Results = schedule;
                string bookingTime = "";
                DateTime? dt = null;

                //check for valid date
                if (model.ContainerId > 0)
                {
                    if (!IsTimeStillAvailable(retval.Results, model.ContainerId))
                    {
                        retval.TimeSlotExpired = true;
                    }

                    //convert JS ticks to .Net date
                    dt = new DateTime(Convert.ToInt64(model.SelectedCaseDate));

                    //set date properties
                    retval.ContainerId = model.ContainerId;
                    retval.SelectedCaseDate = model.SelectedCaseDate;

                    bookingTime = dt.Value.ToString("hh:mm tt") + " to " +
                                         dt.Value.AddMinutes(hearingLength).ToString("hh:mm tt");

                    retval.TimeSlotFriendlyName =
                        dt.Value.ToString("MMMM dd") + " from " + bookingTime;

                }

                _session.ScBookingInfo = new ScSessionBookingInfo
                {
                    ContainerId = model.ContainerId,
                    CaseNumber = model.CaseNumber.ToUpper().Trim(),
                    FullCaseNumber = caseNumber,
                    CaseId = caseId,
                    HearingTypeId = model.HearingTypeId,
                    HearingTypeName = retval.HearingTypeName,
                    HearingLengthMinutes = hearingLength,
                    LocationId = model.SelectedRegistryId,
                    RegistryName = retval.SelectedRegistryName,
                    TimeSlotFriendlyName = bookingTime,
                    SelectedCaseDate = model.SelectedCaseDate,
                    DateFriendlyName = dt?.ToString("dddd, MMMM dd, yyyy") ?? ""
                };
            }

            return retval;
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
        public async Task<string> BuildCaseNumber(string caseId, int locationId)
        {
            //load all locations
            Location[] locations = await _client.getLocationsAsync();

            //fetch location prefix
            string locationPrefix =
                locations.FirstOrDefault(x => x.locationID == locationId)?.locationCode;

            //return location prefix + case number
            return locationPrefix + caseId;
        }

        /// <summary>
        ///     Book court case
        /// </summary>
        public async Task<ScCaseConfirmViewModel> BookCourtCase(ScCaseConfirmViewModel model,
            string userGuid, string userDisplayName)
        {
            //if the user could not be detected return 
            if (string.IsNullOrWhiteSpace(userGuid))
            {
                return model;
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            // check the schedule again to make sure the time slot wasn't taken by someone else
            AvailableDatesByLocation schedule =
                await _client.AvailableDatesByLocationAsync(bookingInfo.LocationId,
                    bookingInfo.HearingTypeId);

            //ensure time slot is still available
            if (IsTimeStillAvailable(schedule, bookingInfo.ContainerId))
            {
                //build object to send to the API
                var bookInfo = new BookHearingInfo
                {
                    caseID = bookingInfo.CaseId,
                    containerID = bookingInfo.ContainerId,
                    dateTime = model.FullDate,
                    hearingLength = bookingInfo.HearingLengthMinutes,
                    locationID = bookingInfo.LocationId,
                    requestedBy = $"{userDisplayName} {model.Phone} {model.EmailAddress}",
                    hearingTypeId = bookingInfo.HearingTypeId
                };

                //submit booking
                BookingHearingResult result = await _client.BookingHearingAsync(bookInfo);
                
                //get the raw result
                bookingInfo.RawResult = result.bookingResult;

                //test to see if the booking was successful
                if (result.bookingResult.ToLower().StartsWith("success"))
                {
                    //create database entry
                    DbSet<BookingHistory> bookingHistory = _dbContext.Set<BookingHistory>();

                    bookingHistory.Add(new BookingHistory
                    {
                        ContainerId = bookingInfo.ContainerId,
                        SmGovUserGuid = userGuid,
                        Timestamp = DateTime.Now
                    });

                    //save to DB
                    _dbContext.SaveChanges();

                    //update model
                    model.IsBooked = true;
                    bookingInfo.IsBooked = true;

                    //store user info in session for next booking
                    var userInfo = new SessionUserInfo
                    {
                        Phone = model.Phone,
                        Email = model.EmailAddress,
                        ContactName = $"{userDisplayName}"
                    };

                    _session.UserInfo = userInfo;

                    //send email
                    await _mailService.SendEmail(
                        model.EmailAddress,
                        EmailSubject,
                        await GetEmailBody());

                    //clear booking info session
                    _session.ScBookingInfo = null;
                }
                else
                {
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
            List<BookingHistory> hearingsBookedForToday = _dbContext.BookingHistory
                .Where(b => b.SmGovUserGuid == userGuid &&
                            b.Timestamp.Day == today.Day &&
                            b.Timestamp.Month == today.Month &&
                            b.Timestamp.Year == today.Year).ToList();

            return MaxHearingsPerDay - hearingsBookedForToday.Count();
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
                CourtFileNumber = _session.ScBookingInfo.CaseNumber,
                RegistryName = booking.RegistryName,
                TypeOfConference = booking.HearingTypeName,
                Date = booking.DateFriendlyName,
                Time = booking.TimeSlotFriendlyName
            };

            //Render the email template
            string template;

            switch (booking.HearingTypeId)
            {
                case ScHearingType.AWS:
                    template = "ScBooking/Email-CV-AWS";
                    break;
                case ScHearingType.JMC:
                    template = "ScBooking/Email-JMC";
                    break;
                case ScHearingType.PTC:
                    template = "ScBooking/Email-CV-PTC";
                    break;
                case ScHearingType.TCH:
                    template = "ScBooking/Email-CV-TCH";
                    break;
                case ScHearingType.TMC:
                    template = "ScBooking/Email-TMC";
                    break;
                default:
                    throw new ArgumentException("Invalid HearingTypeId");
            }

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
                {1, "604-660-2853"},
                {2, "250-356-1450"},
                {3, "604-660-8551"},
                {4, "250-614-2750"},
                {6, "250-828-4351"},
                {7, "250-741-5860"},
                {9, "250-741-5860"},
                {10, "604-795-8349"},
                {11, "250-614-2750"},
                {12, "250-356-1450"},
                {13, "250-614-2750"},
                {15, "250-828-4351"},
                {17, "250-828-4351"},
                {18, "250-470-6935"},
                {20, "250-741-5860"},
                {21, "250-828-4351"},
                {22, "250-741-5860"},
                {24, "250-741-5860"},
                {25, "250-624-7474"},
                {26, "250-470-6935"},
                {27, "250-614-2750"},
                {28, "250-828-4351"},
                {29, "250-828-4351"},
                {30, "250-828-4351"},
                {31, "250-847-7482"},
                {32, "250-624-7474"},
                {33, "250-614-2750"},
                {34, "250-470-6935"}
            };

            return numbers[registryId];
        }
    }
}
