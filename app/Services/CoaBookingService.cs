using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CoaBookingService
    {
        public readonly bool IsLocalDevEnvironment;
        private const string EmailSubject = "BC Courts Booking Confirmation";
        private readonly IOnlineBooking _client;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly SessionService _session;
        private readonly CoaCacheService _coaCacheService;
        private readonly IViewRenderService _viewRenderService;
        private readonly MailService _mailService;

        //Constructor
        public CoaBookingService(
            ApplicationDbContext dbContext,
            IConfiguration configuration,
            SessionService sessionService,
            CoaCacheService coaCacheService,
            IViewRenderService viewRenderService
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
            _configuration = configuration;
            _dbContext = dbContext;
            _session = sessionService;
            _coaCacheService = coaCacheService;
            _viewRenderService = viewRenderService;
            _mailService = new MailService("CA", _configuration, _logger);
        }

        /// <summary>
        ///     Search for available times
        /// </summary>
        public async Task<CoaCaseSearchViewModel> GetSearchResults(CoaCaseSearchViewModel model)
        {
            var retval = model;
            bool invalidCaseNumberFormat = false;
            COACaseList caseNumberResult = new COACaseList();

            //search the current case number
            try
            {
                caseNumberResult = await _client.CoACaseNumberValidAsync(model.CaseNumber);
            }
            catch (FaultException ex)
            {
                if (ex.Message.ToLower().Contains("string was not in a correct format"))
                {
                    invalidCaseNumberFormat = true;
                }
            }

            if (invalidCaseNumberFormat || caseNumberResult.CaseType.ToLower() == "not found")
            {
                //case could not be found
                retval.IsValidCaseNumber = false;
            }
            else
            {
                //case ID
                int caseId = caseNumberResult.CaseList[0].CaseId;
                retval.CaseId = caseId;

                //case type
                string caseType = caseNumberResult.CaseType;
                retval.CaseType = caseType;

                //valid case number
                retval.IsValidCaseNumber = true;

                retval.CaseList = caseNumberResult.CaseList;

                if (caseType == CoaCaseType.Civil)
                {
                    retval.HearingTypeId = 24;
                }
                else if (caseType == CoaCaseType.Criminal)
                {
                    retval.HearingTypeId = model.HearingTypeId;
                }

                if (model.Step2Complete)
                {
                    retval.HearingTypeName =
                        CoaHearingType
                            .GetHearingTypes()
                            .FirstOrDefault(h => h.HearingTypeId == model.HearingTypeId)
                            ?.Description ?? "";
                }

                var bookingInfo = new CoaSessionBookingInfo
                {
                    CaseId = caseId,
                    CaseNumber = model.CaseNumber.ToUpper().Trim(),
                    CaseType = caseType,
                    FactumFiled = model.FactumFiled,
                    DateIsAgreed = model.DateIsAgreed,
                    IsFullDay = model.IsFullDay,
                    HearingTypeName = retval.HearingTypeName,
                    SelectedDate = model.SelectedDate,
                    CaseList = retval.CaseList,
                    SelectedCases = model.SelectedCases,
                    IsAppealHearing = model.IsAppealHearing,
                    SelectedApplicationTypes = model.SelectedApplicationTypes,
                    IsHalfHour = model.IsHalfHour
                };

                if (model.HearingTypeId.HasValue)
                {
                    bookingInfo.HearingTypeId = model.HearingTypeId.Value;
                }

                //check if hearing is chambers and populate the app types if it is
                if (
                    model.IsChambersHearing.GetValueOrDefault(false)
                    && model.ChambersApplicationTypes == null
                )
                {
                    retval.ChambersApplicationTypes =
                        await _coaCacheService.GetChambersApplicationTypesAsync(model.CaseType);
                }

                _session.CoaBookingInfo = bookingInfo;
            }

            return retval;
        }

        /// <summary>
        ///     List available times
        /// </summary>
        public async Task<Dictionary<DateTime, List<DateTime>>> GetAvailableDates(
            string availability,
            bool isAppealHearing
        )
        {
            ShedulesInfo[] dates;
            if (isAppealHearing)
            {
                var availableAppealDates = await _client.COAAvailableDatesAsync();
                dates = availableAppealDates.AvailableDates;
            }
            else
            {
                var availableChambersDates = await _client.CoAAvailableDatesChambersAsync();
                // convert chambers dates to appeal dates (these could have been the same type in the first place)
                dates = availableChambersDates.AvailableDates
                    .Select(
                        date =>
                            new ShedulesInfo
                            {
                                scheduleDate = date.scheduleDate,
                                availability = date.availability
                            }
                    )
                    .ToArray();
            }
            return GroupAvailableDates(dates, availability);
        }

        /// <summary>
        ///     Gets a list of hearing types
        /// </summary>
        public static SelectList GetHearingTypes()
        {
            return new SelectList(
                CoaHearingType
                    .GetHearingTypes()
                    .Where(x => x.IsCriminal)
                    .Select(x => new { Id = x.HearingTypeId, Value = x.Description }),
                "Id",
                "Value"
            );
        }

        /// <summary>
        ///     Check if a time slot is still available for a court booking
        /// </summary>
        public bool IsTimeStillAvailable(CoAAvailableDates schedule, DateTime selectedDate)
        {
            //check if the container ID is still available
            return schedule.AvailableDates.Any(x => x.scheduleDate == selectedDate);
        }

        /// <summary>
        ///     Book court case
        /// </summary>
        public async Task<CoaCaseConfirmViewModel> BookCourtCase(
            CoaCaseConfirmViewModel model,
            string userGuid,
            string userDisplayName
        )
        {
            //if the user could not be detected return
            if (string.IsNullOrWhiteSpace(userGuid))
            {
                return model;
            }

            CoaSessionBookingInfo bookingInfo = _session.CoaBookingInfo;

            // check the schedule again to make sure the time slot wasn't taken by someone else
            CoAAvailableDates schedule = await _client.COAAvailableDatesAsync();

            //ensure time slot is still available
            if (IsTimeStillAvailable(schedule, bookingInfo.SelectedDate.Value))
            {
                //Fetch final main case file after ruling out selection of cases with main case and related cases
                var finalCase = bookingInfo.CaseList
                    .Where(x => x.Case_Num == bookingInfo.CaseNumber)
                    .First();
                var relatedCases = "";
                if (
                    finalCase.Main
                    && model.RelatedCaseList != null
                    && model.RelatedCaseList.Count > 0
                )
                {
                    var relatedCaseIDList = bookingInfo.CaseList
                        .Where(x => model.RelatedCaseList.Contains(x.Case_Num))
                        .Select(x => x.CaseId)
                        .ToList();
                    relatedCases = string.Join("|", relatedCaseIDList);
                }

                //build object to send to the API
                var bookInfo = new CoABookingHearingInfo
                {
                    caseID = finalCase.CaseId,
                    MainCase = finalCase.Main,
                    RelatedCases = relatedCases,
                    email = $"{model.EmailAddress}",
                    hearingDate = DateTime.Parse($"{model.SelectedDate.Value}"),
                    hearingLength = (bookingInfo.IsFullDay ?? false) ? "Full" : "Half",
                    phone = $"{model.Phone}",
                    hearingTypeId = bookingInfo.HearingTypeId,
                    requestedBy = $"{userDisplayName}"
                };

                _logger.Information("BOOKING COURT OF APPEAL => CoAQueueHearingAsync(bookInfo)");
                _logger.Information(JsonSerializer.Serialize(bookInfo));

                //submit booking
                BookingHearingResult result = await _client.CoAQueueHearingAsync(bookInfo);

                //get the raw result
                bookingInfo.RawResult = result.bookingResult;

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
                            Timestamp = DateTime.Now
                        }
                    );

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
                    await _mailService.ExchangeSendEmail(
                        model.EmailAddress,
                        EmailSubject,
                        await GetEmailBody()
                    );

                    //clear booking info session
                    _session.CoaBookingInfo = null;
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
            _session.CoaBookingInfo = bookingInfo;

            return model;
        }

        /// <summary>
        ///     Renders the template for the email body to a string (~/Views/CoaBooking/Email.cshtml)
        /// </summary>
        private async Task<string> GetEmailBody()
        {
            //user information
            SessionUserInfo user = _session.GetUserInformation();

            // booking information
            CoaSessionBookingInfo booking = _session.CoaBookingInfo;

            //set ViewModel for the email
            var viewModel = new EmailViewModel
            {
                EmailAddress = user.Email,
                Phone = user.Phone,
                CourtFileNumber = booking.CaseNumber,
                RelatedCaseList = booking.RelatedCaseList,
                CaseType = booking.CaseType,
                TypeOfConference = booking.HearingTypeName,
                // todo: need to handle chambers 1 hour / half hour
                HearingLength = booking.IsFullDay ?? false ? "Full Day" : "Half Day",
                Date = booking.SelectedDate?.ToString("dddd, MMMM dd, yyyy") ?? "",
                RelatedCasesString = ""
            };

            if (booking.RelatedCaseList != null && booking.RelatedCaseList.Any())
            {
                viewModel.RelatedCasesString =
                    "\nRELATED FILE NUMBER(S):\n"
                    + string.Join(", ", booking.RelatedCaseList)
                    + "\n";
            }

            var template = $"CoaBooking/EmailText";

            //Render the email template
            return await _viewRenderService.RenderToStringAsync(template, viewModel);
        }

        /// <summary>
        ///     Groups Court of Appeal available hearing dates by month and filter by full day if needed
        /// </summary>
        public Dictionary<DateTime, List<DateTime>> GroupAvailableDates(
            ShedulesInfo[] availableDates,
            string availability
        )
        {
            var result = new Dictionary<DateTime, List<DateTime>>();

            IOrderedEnumerable<DateTime> dates;
            if (!string.IsNullOrEmpty(availability))
            {
                dates = availableDates
                    .Where(d => d.availability == availability)
                    .Select(s => s.scheduleDate)
                    .Distinct()
                    .OrderBy(s => s);
            }
            else
            {
                dates = availableDates.Select(s => s.scheduleDate).Distinct().OrderBy(s => s);
            }

            DateTime previousMonth = DateTime.MinValue;

            List<DateTime> availableDatesInMonth = new List<DateTime>();
            DateTime currentMonth = previousMonth;

            foreach (DateTime date in dates)
            {
                currentMonth = new DateTime(date.Year, date.Month, 1);

                if (previousMonth.Month != date.Month || previousMonth.Year != date.Year)
                {
                    availableDatesInMonth = new List<DateTime>();

                    if (currentMonth != DateTime.MinValue)
                    {
                        result.Add(currentMonth, availableDatesInMonth);
                    }
                }

                availableDatesInMonth.Add(date);

                previousMonth = currentMonth;
            }

            return result;
        }
    }
}
