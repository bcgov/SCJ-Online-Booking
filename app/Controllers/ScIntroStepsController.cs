using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.ViewModels.SC;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/sc/[action]")]
    [Authorize]
    public class ScIntroStepsController : Controller
    {
        //Services
        private readonly ScCoreBookingService _coreService;
        private readonly ScTrialBookingService _scTrialBookingService;

        // Strongly typed session
        private readonly SessionService _session;

        //Constructor
        public ScIntroStepsController(
            SessionService sessionService,
            ScCoreBookingService scCoreBookingService,
            ScTrialBookingService scTrialBookingService
        )
        {
            _session = sessionService;
            _coreService = scCoreBookingService;
            _scTrialBookingService = scTrialBookingService;
        }

        [HttpGet]
        [Route("~/booking/sc")]
        public IActionResult Index()
        {
            var model = _coreService.LoadSearchForm();
            return View(model);
        }

        [HttpGet]
        [Route("~/booking/sc/select-case")]
        public IActionResult SelectCaseAsync()
        {
            var model = _coreService.ReloadSearchForm();
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

            if (string.IsNullOrWhiteSpace(model.CaseNumber))
            {
                ModelState.AddModelError("CaseNumber", "Please provide a Court File Number");
            }
            else if (!model.CaseNumber.All(char.IsDigit))
            {
                ModelState.AddModelError("CaseNumber", "Invalid number");
            }

            model.CaseLocationName = await _coreService.GetLocationNameAsync(model.CaseRegistryId);

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            model = await _coreService.GetSearchResults(model);

            return View("Index", model);
        }

        [HttpPost]
        [Route("~/booking/sc/case-selected")]
        public async Task<IActionResult> CaseSelectedAsync(ScCaseSearchViewModel model)
        {
            model.IsConfirmingCase = true;

            if (model.SelectedPhysicalFileId == 0m)
            {
                ModelState.AddModelError("SelectedPhysicalFileId", "Please choose a case.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            await _coreService.SaveSearchForm(model);

            return RedirectToAction("BookingType");
        }

        [HttpGet]
        [Route("~/booking/sc/booking-type")]
        public async Task<IActionResult> BookingType()
        {
            var model = _coreService.LoadBookingTypeForm();

            if (model.SessionInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            model.HasExistingTrialRequest =
                await _scTrialBookingService.CheckIfTrialAlreadyRequestedAsync();
            model.AvailableBookingTypes = await _coreService.GetAvailableBookingTypesAsync();

            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc/booking-type")]
        public async Task<IActionResult> BookingType(ScBookingTypeViewModel model)
        {
            if (model.HearingTypeId == -1)
            {
                ModelState.AddModelError("HearingTypeId", "Please choose what you are booking.");
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

                if (model.EstimatedTrialLength > 40)
                {
                    ModelState.AddModelError(
                        "EstimatedTrialLength",
                        "Estimated trial length must be between 1 and 40 days."
                    );
                }
            }

            // Extra fields for "Long Chambers" booking type
            if (model.HearingTypeId == ScHearingType.LONG_CHAMBERS)
            {
                if (model.EstimatedChambersLength == null || model.EstimatedChambersLength == 0)
                {
                    ModelState.AddModelError(
                        "EstimatedChambersLength",
                        "Provide the requested days of your chambers hearing."
                    );
                }
            }

            if (model.HearingTypeId == ScHearingType.TRIAL)
            {
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

                    if (model.AlternateLocationRegistryId == -1)
                    {
                        ModelState.AddModelError(
                            "AlternateLocationRegistryId",
                            "Please choose a trial location."
                        );
                    }
                }
            }

            if (model.HearingTypeId == ScHearingType.LONG_CHAMBERS)
            {
                if (model.IsHomeRegistry == null)
                {
                    ModelState.AddModelError(
                        "IsHomeRegistry",
                        "Indicate if the chambers is taking place in the home registry."
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

                    if (model.AlternateLocationRegistryId == -1)
                    {
                        ModelState.AddModelError(
                            "AlternateLocationRegistryId",
                            "Please choose a location for the chambers hearing."
                        );
                    }
                }
            }

            if (model.IsHomeRegistry == false && model.IsLocationChangeFiled == false)
            {
                // Fallback for js validation. Triggers page error state w/o field message
                char space = (char)32;
                ModelState.AddModelError("IsLocationChangeFiled", space.ToString());
            }

            if (!ModelState.IsValid)
            {
                model.AvailableBookingTypes = await _coreService.GetAvailableBookingTypesAsync();
                model.SessionInfo = _session.ScBookingInfo;
                return View(model);
            }

            await _coreService.SaveBookingTypeFormAsync(model);

            if (model.HearingTypeId == ScHearingType.TRIAL)
            {
                return RedirectToAction("AvailableDates", "ScTrial");
            }
            else if (model.HearingTypeId == ScHearingType.LONG_CHAMBERS)
            {
                return RedirectToAction("AvailableDates", "ScLongChambers");
            }
            else
            {
                return RedirectToAction("AvailableTimes", "ScConference");
            }
        }

        /// <summary>
        /// This is just for debugging purposes to see headers added by reverse proxies
        /// </summary>
        [HttpGet]
        public IActionResult Headers()
        {
            return View();
        }

        /// <summary>
        /// This is just for debugging purposes to see the supreme court session object
        /// </summary>
        [HttpGet]
        public IActionResult Session()
        {
            var bookingInfo = _session.ScBookingInfo;
            return View(bookingInfo);
        }
    }
}
