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
    public class BookingController : Controller
    {
        //Services
        private readonly BookingService _bookingService;

        //HttpContext
        private readonly HttpContext _httpContext;

        // Strongly typed session
        private readonly SessionService _session;

        //Give us access to the HostEnvironment properties
        private readonly IViewRenderService _viewRenderService;

        //Constructor
        public BookingController(ApplicationDbContext context, IHttpContextAccessor httpAccessor,
            IConfiguration configuration, SessionService sessionService, IViewRenderService viewRenderService)
        {
            _httpContext = httpAccessor.HttpContext;
            _viewRenderService = viewRenderService;
            _session = sessionService;
            _bookingService = new BookingService(context, httpAccessor, configuration, sessionService);
        }

        [HttpGet]
        public async Task<IActionResult> CaseSearch()
        {
            //Populate dropdown list values
            return View(await _bookingService.LoadSearchForm());
        }

        [HttpPost]
        public async Task<IActionResult> CaseSearch(CaseSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model = await _bookingService.GetSearchResults(model);

            //test if the user selected a time-slot that is available
            if (model != null && model.ContainerId > 0 && !model.TimeSlotExpired)
                //go to confirmation screen
            {
                return new RedirectResult("/scjob/booking/CaseConfirm");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CaseConfirm()
        {
            SessionBookingInfo bookingInfo = _session.BookingInfo;

            //convert JS ticks to .Net date
            var dt = new DateTime(Convert.ToInt64(bookingInfo.SelectedCaseDate));

            //user information
            var sui = _bookingService.GetUserInformation();

            //Time-slot is still available
            var ccm = new CaseConfirmViewModel
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
        public async Task<IActionResult> CaseBooked(CaseConfirmViewModel model)
        {
            string userGuid;
            string userDisplayName;

            if (_bookingService.IsLocalDevEnvironment)
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
            return View(
                await _bookingService.BookCourtCase(model, userGuid, userDisplayName, _viewRenderService));
        }

        [HttpGet]
        public IActionResult Headers()
        {
            return View();
        }
    }
}
