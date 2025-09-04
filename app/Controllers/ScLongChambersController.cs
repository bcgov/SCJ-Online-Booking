using System;
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
    [Route("booking/sc-long-chambers/[action]")]
    [Authorize]
    public class ScLongChambersController : Controller
    {
        //Services
        private readonly ScCoreService _scCoreService;
        private readonly ScFairBookingService _scFairBookingService;

        // Strongly typed session
        private readonly SessionService _session;

        //Constructor
        public ScLongChambersController(
            SessionService sessionService,
            ScCoreService scCoreService,
            ScFairBookingService scFairBookingService
        )
        {
            _session = sessionService;
            _scCoreService = scCoreService;
            _scFairBookingService = scFairBookingService;
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/available-times")]
        public async Task<IActionResult> AvailableTimesAsync()
        {
            var model = await _scFairBookingService.LoadAvailableTimesFormAsync();

            if (string.IsNullOrWhiteSpace(model.CaseNumber))
            {
                return RedirectToAction("Index");
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            // Trial bookings: get lists of available trial dates
            (model.AvailableRegularTrialDates, bookingInfo.RegularFormula) =
                await _scFairBookingService.GetAvailableTrialDatesAsync(
                    ScFormulaType.RegularBooking,
                    bookingInfo.RegularFormula
                );

            (model.AvailableFairUseTrialDates, bookingInfo.FairUseFormula) =
                await _scFairBookingService.GetAvailableTrialDatesAsync(
                    ScFormulaType.FairUseBooking,
                    bookingInfo.FairUseFormula
                );

            _session.ScBookingInfo = bookingInfo;

            if (bookingInfo.FairUseFormula is null)
            {
                model.TrialFormulaType = ScFormulaType.RegularBooking;
            }
            else
            {
                model.TrialFormulaType =
                    bookingInfo.TrialFormulaType ?? ScFormulaType.FairUseBooking;
            }

            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc-long-chambers/available-times")]
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

            if (
                model.TrialFormulaType == ScFormulaType.RegularBooking
                && !model.SelectedRegularTrialDate.HasValue
            )
            {
                ModelState.AddModelError(
                    "SelectedRegularTrialDate",
                    "Please choose from one of the available dates."
                );
            }
            else if (
                model.TrialFormulaType == ScFormulaType.FairUseBooking
                && model.SelectedFairUseTrialDates.Count == 0
            )
            {
                ModelState.AddModelError(
                    "SelectedFairUseTrialDates",
                    "Please choose from the available dates."
                );
            }
            else if (model.TrialFormulaType == "")
            {
                // If the formula type field is empty
                // (e.g. user tampered with the form or submitted without JavaScript)
                ModelState.AddModelError("TrialFormulaType", "Please choose what you are booking.");
            }

            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;
                // Trial bookings: get lists of available trial dates

                model = await _scFairBookingService.LoadAvailableTimesFormulaInfoAsync(
                    model,
                    bookingInfo.FairUseFormula
                );

                (model.AvailableRegularTrialDates, _) =
                    await _scFairBookingService.GetAvailableTrialDatesAsync(
                        ScFormulaType.RegularBooking,
                        null
                    );

                (model.AvailableFairUseTrialDates, _) =
                    await _scFairBookingService.GetAvailableTrialDatesAsync(
                        ScFormulaType.FairUseBooking,
                        null
                    );

                return View(model);
            }

            _scFairBookingService.SaveAvailableTimesFormAsync(model);

            return RedirectToAction("CaseConfirm");
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/case-confirm")]
        public async Task<IActionResult> CaseConfirmAsync()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            //user information
            var user = _session.GetUserInformation();

            string locationName;
            if (bookingInfo.HearingTypeId == ScHearingType.TRIAL)
            {
                locationName = await _scCoreService.GetLocationNameAsync(
                    bookingInfo.TrialLocationRegistryId
                );
            }
            else
            {
                locationName = await _scCoreService.GetLocationNameAsync(
                    bookingInfo.BookingLocationRegistryId
                );
            }

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
        [Route("~/booking/sc-long-chambers/case-confirm")]
        public async Task<IActionResult> CaseConfirm(ScCaseConfirmViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;
            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;
                return View(model);
            }

            ClaimsPrincipal user = HttpContext.User;

            try
            {
                await _scFairBookingService.BookTrialAsync(model, user);

                if (bookingInfo.TrialFormulaType == ScFormulaType.RegularBooking)
                {
                    // Redirect to "TrialBooked" page for Regular
                    return RedirectToAction("TrialBooked");
                }
                else
                {
                    // Redirect to "RequestSubmitted" page for Fair-Use
                    return RedirectToAction("RequestSubmitted");
                }
            }
            catch (InvalidOperationException ex)
            {
                string errorMessage = ex.Message;
                ModelState.AddModelError("SelectedRegularTrialDate", errorMessage);
                model.SessionInfo = bookingInfo;
                return View(model);
            }
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/trial-booked")]
        public IActionResult TrialBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/trial-request-submitted")]
        public IActionResult RequestSubmitted()
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
