using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.MVC.Data;
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

        //HttpContext
        private readonly HttpContext _httpContext;

        // Strongly typed session
        private readonly SessionService _session;

        //Give us access to the HostEnvironment properties
        private readonly IViewRenderService _viewRenderService;

        //Constructor
        public ScBookingController(ApplicationDbContext context, IHttpContextAccessor httpAccessor,
            IConfiguration configuration, SessionService sessionService, IViewRenderService viewRenderService)
        {
            _httpContext = httpAccessor.HttpContext;
            _viewRenderService = viewRenderService;
            _session = sessionService;
            _scBookingService = new ScBookingService(context, httpAccessor, configuration, sessionService, viewRenderService);
        }

        [HttpGet]
        public async Task<IActionResult> CaseSearch()
        {
            //Populate dropdown list values
            return View(await _scBookingService.LoadSearchForm());
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
                return Redirect("/scjob/booking/sc/CaseSearch");
            }

            //convert JS ticks to .Net date
            var dt = new DateTime(Convert.ToInt64(bookingInfo.SelectedCaseDate));

            //user information
            var sui = _scBookingService.GetUserInformation();

            //Time-slot is still available
            var ccm = new ScCaseConfirmViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                Date = dt.ToString("dddd, MMMM dd, yyyy"),
                Time = bookingInfo.TimeSlotFriendlyName,
                LocationName = $"{bookingInfo.RegistryName} Law Courts",
                HearingTypeName = bookingInfo.HearingTypeName,
                ContainerId = bookingInfo.ContainerId,
                LocationId = bookingInfo.LocationId,
                FullDate = dt,
                IsUserKnown = true,
                EmailAddress =  sui.Email,
                Phone = sui.Phone
            };

            return View(ccm);
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
                userGuid = _httpContext.Request.Headers.ContainsKey("smgov_userguid")
                    ? _httpContext.Request.Headers["smgov_userguid"].ToString()
                    : string.Empty;

                //read smgov_userdisplayname SiteMinder header
                userDisplayName = _httpContext.Request.Headers.ContainsKey("smgov_userdisplayname")
                    ? _httpContext.Request.Headers["smgov_userdisplayname"].ToString()
                    : string.Empty;
            }

            //make booking
            var result = 
                await _scBookingService.BookCourtCase(model, userGuid, userDisplayName);

            return Redirect(
                $"/scjob/booking/sc/CaseBooked?booked={(result.IsBooked ? "true" : "false")}");
        }

        [HttpGet]
        public IActionResult CaseBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (string.IsNullOrEmpty(bookingInfo.CaseNumber))
            {
                return Redirect("/scjob/booking/sc/CaseSearch");
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
