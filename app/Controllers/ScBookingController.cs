using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/sc/[action]")]
    public class ScBookingController : Controller
    {
        //Services
        private readonly ScBookingService _scBookingService;

        // Strongly typed session
        private readonly SessionService _session;

        private readonly string step1Url = "/scjob/booking/sc";

        //Constructor
        public ScBookingController(SessionService sessionService, ScBookingService scBookingService)
        {
            _session = sessionService;
            _scBookingService = scBookingService;
        }

        [HttpGet]
        [Route("~/booking/sc")]
        [Route("~/booking/sc/Index")]
        public IActionResult Index(bool s = true)
        {
            var model = s ?
                _scBookingService.LoadSearchForm() :
                _scBookingService.LoadSearchForm2();
            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc")]
        [Route("~/booking/sc/Index")]
        public async Task<IActionResult> Index(ScCaseSearchViewModel model)
        {
            if (model.CaseRegistryId == -1)
            {
                ModelState.AddModelError("CaseRegistryId", "Please select the registry where the file was created");
            }

            if (string.IsNullOrWhiteSpace(model.CaseNumber))
            {
                ModelState.AddModelError("CaseNumber", "Please provide a Court File Number");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model = await _scBookingService.GetSearchResults2(model);

            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc/case-selected")]
        public async Task<IActionResult> CaseSelectedAsync(ScCaseSearchViewModel model)
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

            await _scBookingService.SaveScBookingInfoAsync(model); 

            return RedirectToAction("ConferenceType");
        }

        [HttpGet]
        [Route("~/booking/sc/conference-type")]
        public async Task<IActionResult> ConferenceType()
        {
            var model = _scBookingService.LoadSearchForm2();

            if (string.IsNullOrEmpty(model.CaseNumber))
            {
                return Redirect(step1Url);
            }

            model.AvailableConferenceTypeIds =
                await _scBookingService.GetConferenceTypesAsync(model.CaseLocationName);
            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc/conference-type")]
        public async Task<IActionResult> ConferenceType(ScCaseSearchViewModel model)
        {
            if (model.HearingTypeId == -1)
            {
                ModelState.AddModelError("HearingTypeId", "Please choose a conference type.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _scBookingService.SaveScBookingInfoAsync(model);

            return RedirectToAction("AvailableTimes");
        }

        [HttpGet]
        [Route("~/booking/sc/available-times")]
        public IActionResult AvailableTimes()
        {
            var model = _scBookingService.LoadSearchForm2();

            if (string.IsNullOrEmpty(model.CaseNumber))
            {
                return Redirect(step1Url);
            }

            return View(model);
        }

        [HttpPost]
        [Route("~/booking/sc/available-times")]
        public async Task<IActionResult> AvailableTimes(ScCaseSearchViewModel model)
        {
            if (model.ContainerId == -1)
            {
                ModelState.AddModelError("ContainerId", "Please choose from one of the available times.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Results = _session.ScBookingInfo.Results;
            await _scBookingService.SaveScBookingInfoAsync(model);

            return RedirectToAction("CaseConfirm");
        }

        [HttpGet]
        public IActionResult CaseSearch()
        {
            return View(_scBookingService.LoadSearchForm());
        }

        [HttpPost]
        public async Task<IActionResult> CaseSearch(ScCaseSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model = await _scBookingService.GetSearchResults(model);

            //test if the user selected a time-slot that is available
            if (model != null && model.ContainerId > 0 && !model.TimeSlotExpired)
                //go to confirmation screen
            {
                return new RedirectResult("/scjob/booking/sc/CaseConfirm");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CaseConfirm()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (string.IsNullOrEmpty(bookingInfo.CaseNumber))
            {
                return Redirect(step1Url);
            }

            //user information
            var user = _session.GetUserInformation();

            //Time-slot is still available
            var model = new ScCaseConfirmViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                Date = bookingInfo.DateFriendlyName, 
                Time = bookingInfo.TimeSlotFriendlyName,
                CaseLocationName = $"{bookingInfo.CaseLocationName} Law Courts",
                BookingLocationName = $"{bookingInfo.BookingLocationName} Law Courts",
                HearingTypeName = bookingInfo.HearingTypeName,
                ContainerId = bookingInfo.ContainerId,
                CaseRegistryId = bookingInfo.CaseRegistryId,
                BookingRegistryId = bookingInfo.BookingRegistryId,
                FullDate = bookingInfo.FullDate, 
                EmailAddress =  user.Email,
                Phone = user.Phone
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CaseConfirm(ScCaseConfirmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userGuid;
            string userDisplayName;

            if (_scBookingService.IsLocalDevEnvironment)
            {
                // use fake SiteMinder header values for local development
                userGuid = "072cfc73-e3b9-437b-8012-0b0945f09879";
                userDisplayName = "Matthew Begbie";
            }
            else
            {
                //read smgov_userguid SiteMinder header
                userGuid = HttpContext.Request.Headers.ContainsKey("smgov_userguid")
                    ? HttpContext.Request.Headers["smgov_userguid"].ToString()
                    : string.Empty;

                //read smgov_userdisplayname SiteMinder header
                userDisplayName = HttpContext.Request.Headers.ContainsKey("smgov_userdisplayname")
                    ? HttpContext.Request.Headers["smgov_userdisplayname"].ToString()
                    : string.Empty;
            }

            //make booking
            var result = await _scBookingService.BookCourtCase(model, userGuid, userDisplayName);

            return Redirect(
                $"/scjob/booking/sc/CaseBooked?booked={result.IsBooked}");
        }

        [HttpGet]
        public IActionResult CaseBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (string.IsNullOrEmpty(bookingInfo.CaseNumber))
            {
                return Redirect(step1Url);
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
