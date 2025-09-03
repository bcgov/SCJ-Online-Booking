using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/sc-conference/[action]")]
    [Authorize]
    public class ScConferenceController : Controller
    {
        //Services
        private readonly ScCoreService _scCoreService;
        private readonly ScConferenceBookingService _scConferenceBookingService;

        // Strongly typed session
        private readonly SessionService _session;

        //Constructor
        public ScConferenceController(
            SessionService sessionService,
            ScCoreService scCoreService,
            ScConferenceBookingService scConferenceBookingService
        )
        {
            _session = sessionService;
            _scCoreService = scCoreService;
            _scConferenceBookingService = scConferenceBookingService;
        }

        [HttpGet]
        [Route("~/booking/sc-conference/available-times")]
        public async Task<IActionResult> AvailableTimesAsync()
        {
            var model = await _scCoreService.LoadAvailableTimesFormAsync();

            if (string.IsNullOrWhiteSpace(model.CaseNumber))
            {
                return RedirectToAction("Index");
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc-conference/available-times")]
        public async Task<IActionResult> AvailableTimesAsync(ScAvailableTimesViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;
            model.AvailableConferenceDates = bookingInfo.AvailableConferenceDates;

            // Require ContainerId value for non-trial hearing types
            if (model.ContainerId == -1)
            {
                ModelState.AddModelError(
                    "ContainerId",
                    "Please choose from one of the available times."
                );
            }

            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;
                return View(model);
            }

            await _scCoreService.SaveAvailableTimesFormAsync(model);

            return RedirectToAction("CaseConfirm");
        }

        [HttpGet]
        [Route("~/booking/sc-conference/case-confirm")]
        public async Task<IActionResult> CaseConfirmAsync()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            //user information
            var user = _session.GetUserInformation();

            string locationName = await _scCoreService.GetLocationNameAsync(
                bookingInfo.BookingLocationRegistryId
            );

            //Time-slot is still available
            var model = new ScCaseConfirmViewModel
            {
                Date = bookingInfo.FormattedConferenceDate,
                Time = bookingInfo.FormattedConferenceTime,
                TrialLocationName = locationName,
                SelectedRegularTrialDate = bookingInfo.SelectedRegularTrialDate,
                FullDate = bookingInfo.SelectedConferenceDate,
                EmailAddress = user.Email,
                Phone = user.Phone,
                SessionInfo = bookingInfo
            };

            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc-conference/case-confirm")]
        public async Task<IActionResult> CaseConfirm(ScCaseConfirmViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;
            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;
                return View(model);
            }

            ClaimsPrincipal user = HttpContext.User;

            var result = await _scConferenceBookingService.BookConferenceAsync(model, user);

            return Redirect(
                $"/scjob/booking/sc-conference/conference-booked?booked={result.IsBooked}"
            );
        }

        [HttpGet]
        [Route("~/booking/sc-conference/conference-booked")]
        public IActionResult ConferenceBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
