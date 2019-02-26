using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
                return RedirectToAction("CaseConfirm");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CaseConfirm()
        {
            SessionBookingInfo bookingInfo = _session.BookingInfo;

            //convert JS ticks to .Net date
            var dt = new DateTime(Convert.ToInt64(bookingInfo.SelectedCaseDate));

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
                EmailAddress = _httpContext.Request.Headers.ContainsKey("SMGOV-USEREMAIL")
                    ? _httpContext.Request.Headers["SMGOV-USEREMAIL"].ToString()
                    : string.Empty,
                Phone = _httpContext.Request.Headers.ContainsKey("SMGOV-USERPHONE")
                    ? _httpContext.Request.Headers["SMGOV-USERPHONE"].ToString()
                    : string.Empty
            };

            return View(ccm);
        }

        [HttpPost]
        public async Task<IActionResult> CaseBooked(CaseConfirmViewModel model)
        {
            SessionBookingInfo bookingInfo = _session.BookingInfo;

            // fake user id for testing without BCeID
            var userId = "B8C1EC79-6464-4C62-BF33-05FC00CC21A0";

            //read user-guid in headers
            if (_httpContext.Request.Headers.ContainsKey("SMGOV-USERGUID"))
            {
                userId = _httpContext.Request.Headers["SMGOV-USERGUID"].ToString();
            }

            //make booking
            return View(
                await _bookingService.BookCourtCase(model, bookingInfo.HearingTypeId,
                    bookingInfo.HearingLengthMinutes, userId, _viewRenderService));
        }
    }
}
