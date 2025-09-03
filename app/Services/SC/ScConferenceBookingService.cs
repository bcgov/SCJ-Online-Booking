using System;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
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
    public class ScConferenceBookingService
    {
        public readonly bool IsLocalDevEnvironment;

        private readonly IOnlineBooking _client;
        private readonly ILogger _logger;
        private readonly SessionService _session;
        private readonly DataWriterService _dbWriterService;
        private readonly IViewRenderService _viewRenderService;
        private readonly MailQueueService _mailService;

        //Constructor
        public ScConferenceBookingService(
            ApplicationDbContext dbContext,
            IConfiguration configuration,
            SessionService sessionService,
            IViewRenderService viewRenderService,
            ScCacheService scCacheService,
            ScCoreService coreService
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
            _mailService = new MailQueueService(configuration, dbContext);
            _dbWriterService = new DataWriterService(dbContext);
        }

        /// <summary>
        ///     Book court case
        /// </summary>
        public async Task<ScCaseConfirmViewModel> BookConferenceAsync(
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
                bookingInfo.BookingLocationRegistryId,
                bookingInfo.HearingTypeId
            );

            //ensure time slot is still available
            if (ScCoreService.IsTimeStillAvailable(schedule, bookingInfo.ContainerId))
            {
                string userDisplayName = OpenIdConnectHelper.GetUserFullName(user);
                long userId = long.Parse(user.FindFirst(ClaimTypes.Sid)?.Value ?? "0");

                //build object to send to the API
                var requestPayload = new BookHearingInfo
                {
                    CEIS_Physical_File_ID = bookingInfo.PhysicalFileId,
                    containerID = bookingInfo.ContainerId,
                    dateTime = bookingInfo.SelectedConferenceDate,
                    hearingLength = bookingInfo.ConferenceLengthMinutes,
                    locationID = bookingInfo.BookingLocationRegistryId,
                    requestedBy = $"{userDisplayName} {model.Phone} {model.EmailAddress}",
                    hearingTypeId = bookingInfo.HearingTypeId
                };

                _logger.Information("BOOKING SUPREME COURT => BookingHearingAsync()");
                _logger.Information(JsonSerializer.Serialize(requestPayload));

                //submit booking
                BookingHearingResult result = await _client.BookingHearingAsync(requestPayload);

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

                    var emailBody = await GetConferenceEmailBodyAsync();
                    const string emailSubject = "BC Courts Online Booking Confirmation";

                    //send email
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
        ///     Renders the template for the email body to a string (~/Views/ScCore/Email.cshtml)
        /// </summary>
        private async Task<string> GetConferenceEmailBodyAsync()
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
                Time = new HtmlString(booking.FormattedConferenceTime)
            };

            //Render the email template
            string template = booking.HearingTypeId switch
            {
                ScHearingType.AWS => "ScCore/Emails/Email-CV-AWS",
                ScHearingType.JMC => "ScCore/Emails/Email-JMC",
                ScHearingType.PTC => "ScCore/Emails/Email-CV-PTC",
                ScHearingType.TCH => "ScCore/Emails/Email-CV-TCH",
                ScHearingType.TMC => "ScCore/Emails/Email-TMC",
                ScHearingType.CPC => "ScCore/Emails/Email-CPC",
                ScHearingType.JCC => "ScCore/Emails/Email-JCC",
                _ => throw new ArgumentException("Invalid HearingTypeId"),
            };
            return await _viewRenderService.RenderToStringAsync(template, viewModel);
        }
    }
}
