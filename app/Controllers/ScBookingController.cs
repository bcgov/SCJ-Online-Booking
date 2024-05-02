using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Constants;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/sc/[action]")]
    [Authorize]
    public class ScBookingController : Controller
    {
        //Services
        private readonly ScCoreService _scCoreService;
        private readonly ScTrialBookingService _scTrialBookingService;
        private readonly ScConferenceBookingService _scConferenceBookingService;

        // Strongly typed session
        private readonly SessionService _session;

        //Constructor
        public ScBookingController(
            SessionService sessionService,
            ScCoreService scCoreService,
            ScTrialBookingService scTrialBookingService,
            ScConferenceBookingService scConferenceBookingService
        )
        {
            _session = sessionService;
            _scCoreService = scCoreService;
            _scTrialBookingService = scTrialBookingService;
            _scConferenceBookingService = scConferenceBookingService;
        }

        [HttpGet]
        [Route("~/booking/sc")]
        public IActionResult Index()
        {
            var model = _scCoreService.LoadSearchForm();
            return View(model);
        }

        [HttpGet]
        [Route("~/booking/sc/select-case")]
        public IActionResult SelectCaseAsync()
        {
            var model = _scCoreService.ReloadSearchForm();
            return View("Index", model);
        }

        [HttpGet]
        [Route("~/booking/sc/search")]
        public async Task<IActionResult> Search(ScCaseSearchViewModel model)
        {
            if (model.CaseRegistryId == -1)
            {
                ModelState.AddModelError(
                    "CaseRegistryId",
                    "Please select the registry where the file was created"
                );
            }

            if (!model.CaseNumber.HasValue)
            {
                ModelState.AddModelError("CaseNumber", "Please provide a Court File Number");
            }

            model.CaseLocationName = await _scCoreService.GetLocationNameAsync(
                model.CaseRegistryId
            );

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            model = await _scCoreService.GetSearchResults(model);

            return View("Index", model);
        }

        [HttpPost]
        [Route("~/booking/sc/case-selected")]
        public IActionResult CaseSelectedAsync(ScCaseSearchViewModel model)
        {
            model.IsConfirmingCase = true;

            if (model.SelectedCaseId == 0)
            {
                ModelState.AddModelError("SelectedCaseId", "Please choose a case.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            _scCoreService.SaveSearchForm(model);

            return RedirectToAction("BookingType");
        }

        [HttpGet]
        [Route("~/booking/sc/booking-type")]
        public async Task<IActionResult> BookingType()
        {
            var model = _scCoreService.LoadBookingTypeForm();

            if (model.SessionInfo.CaseNumber == 0)
            {
                return RedirectToAction("Index");
            }

            model.HasExistingTrialRequest =
                await _scTrialBookingService.CheckIfTrialAlreadyRequestedAsync();
            model.AvailableBookingTypes = await _scCoreService.GetAvailableBookingTypesAsync();

            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc/booking-type")]
        public async Task<IActionResult> BookingType(ScBookingTypeViewModel model)
        {
            if (model.HearingTypeId == -1)
            {
                ModelState.AddModelError("HearingTypeId", "Please choose a booking type.");
            }

            // Extra fields for "Trial" booking type
            if (model.HearingTypeId == ScHearingType.TRIAL)
            {
                if (model.EstimatedTrialLength == null || model.EstimatedTrialLength == 0)
                {
                    ModelState.AddModelError(
                        "EstimatedTrialLength",
                        "Provide the estimated length of your trial."
                    );
                }

                if (model.IsHomeRegistry == null)
                {
                    ModelState.AddModelError(
                        "IsHomeRegistry",
                        "Indicate if the trial is taking place in the home registry."
                    );
                }

                if (model.IsHomeRegistry == false)
                {
                    if (model.IsLocationChangeFiled == null)
                    {
                        ModelState.AddModelError(
                            "IsLocationChangeFiled",
                            "Please choose an option."
                        );
                    }

                    if (model.TrialLocationRegistryId == -1)
                    {
                        ModelState.AddModelError(
                            "TrialLocationRegistryId",
                            "Please choose a trial location."
                        );
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                model.AvailableBookingTypes = await _scCoreService.GetAvailableBookingTypesAsync();
                model.SessionInfo = _session.ScBookingInfo;
                return View(model);
            }

            await _scCoreService.SaveBookingTypeFormAsync(model);

            return RedirectToAction("AvailableTimes");
        }

        [HttpGet]
        [Route("~/booking/sc/available-times")]
        public async Task<IActionResult> AvailableTimesAsync()
        {
            var model = await _scCoreService.LoadAvailableTimesFormAsync();

            if (model.CaseNumber == 0)
            {
                return RedirectToAction("Index");
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            // Trial bookings: get lists of available trial dates
            if (bookingInfo.HearingTypeId == ScHearingType.TRIAL)
            {
                (model.AvailableRegularTrialDates, bookingInfo.RegularFormula) =
                    await _scTrialBookingService.GetAvailableTrialDatesAsync(
                        ScFormulaType.RegularBooking,
                        bookingInfo.RegularFormula
                    );

                (model.AvailableFairUseTrialDates, bookingInfo.FairUseFormula) =
                    await _scTrialBookingService.GetAvailableTrialDatesAsync(
                        ScFormulaType.FairUseBooking,
                        bookingInfo.FairUseFormula
                    );

                _session.ScBookingInfo = bookingInfo;
            }

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
        [Route("~/booking/sc/available-times")]
        public async Task<IActionResult> AvailableTimesAsync(ScAvailableTimesViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;
            model.AvailableConferenceDates = bookingInfo.AvailableConferenceDates;

            // Require ContainerId value for non-trial hearing types
            if (model.HearingTypeId != ScHearingType.TRIAL && model.ContainerId == -1)
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

            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;
                // Trial bookings: get lists of available trial dates
                if (model.SessionInfo.HearingTypeId == ScHearingType.TRIAL)
                {
                    model = await _scCoreService.LoadAvailableTimesFormulaInfoAsync(
                        model,
                        bookingInfo.FairUseFormula
                    );

                    (model.AvailableRegularTrialDates, _) =
                        await _scTrialBookingService.GetAvailableTrialDatesAsync(
                            ScFormulaType.RegularBooking,
                            null
                        );

                    (model.AvailableFairUseTrialDates, _) =
                        await _scTrialBookingService.GetAvailableTrialDatesAsync(
                            ScFormulaType.FairUseBooking,
                            null
                        );
                }
                return View(model);
            }

            await _scCoreService.SaveAvailableTimesFormAsync(model);

            return RedirectToAction("CaseConfirm");
        }

        [HttpGet]
        [Route("~/booking/sc/case-confirm")]
        public async Task<IActionResult> CaseConfirmAsync()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.CaseNumber == 0)
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
        [Route("~/booking/sc/case-confirm")]
        public async Task<IActionResult> CaseConfirm(ScCaseConfirmViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;
            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;
                return View(model);
            }

            ClaimsPrincipal user = HttpContext.User;

            // book trial or court case and redirect to "booked" page
            if (bookingInfo.HearingTypeId == ScHearingType.TRIAL)
            {
                try
                {
                    await _scTrialBookingService.BookTrialAsync(model, user);

                    if (bookingInfo.TrialFormulaType == ScFormulaType.RegularBooking)
                    {
                        // Redirect to "TrialBooked" page for Regular
                        return Redirect("/scjob/booking/sc/trial-booked");
                    }
                    else
                    {
                        // Redirect to "RequestSubmitted" page for Fair-Use
                        return Redirect("/scjob/booking/sc/trial-request-submitted");
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
            else
            {
                var result = await _scConferenceBookingService.BookConferenceAsync(model, user);

                return Redirect($"/scjob/booking/sc/conference-booked?booked={result.IsBooked}");
            }
        }

        [HttpGet]
        [Route("~/booking/sc/conference-booked")]
        public IActionResult ConferenceBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.CaseNumber == 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("~/booking/sc/trial-booked")]
        public IActionResult TrialBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.CaseNumber == 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("~/booking/sc/trial-request-submitted")]
        public IActionResult RequestSubmitted()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.CaseNumber == 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Headers()
        {
            return View();
        }
    }
}
