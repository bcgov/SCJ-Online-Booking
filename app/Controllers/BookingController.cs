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

        //Environment
        private readonly bool _isLocalDevEnvironment;

        //Constructor
        public BookingController(ApplicationDbContext context, IHttpContextAccessor httpAccessor,
            IConfiguration configuration)
        {
            _httpContext = httpAccessor.HttpContext;
            _bookingService = new BookingService(context, httpAccessor, configuration);

            //test the environment
            if (configuration["TAG_NAME"].ToLower().Equals("localdev"))
            {
                _isLocalDevEnvironment = true;
            }
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

            //get results from services layer. 
            int hearingLength = await _bookingService.GetLocationHearingLength(model.SelectedRegistryId, HearingType.TMC);

            model = await _bookingService.GetSearchResults(model, HearingType.TMC, hearingLength);

            //test if the user selected a time-slot that is available
            if (model != null && model.ContainerId > 0 && !model.TimeSlotExpired)
                //go to confirmation screen
            {
                return RedirectToAction("CaseConfirm",
                    new
                    {
                        caseId = model.CaseNumber,
                        locationId = model.SelectedRegistryId,
                        containerId = model.ContainerId,
                        bookingTime = model.SelectedCaseDate
                    });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CaseConfirm(string caseId, int locationId, int containerId,
            string bookingTime)
        {
            //convert JS ticks to .Net date
            var dt = new DateTime(Convert.ToInt64(bookingTime));

            //Time-slot is still available
            var ccm = new CaseConfirmViewModel
            {
                CaseNumber = caseId,
                Date = dt.ToString("dddd, MMMM dd, yyyy"),
                Time = dt.ToString("hh:mm tt") + " - " + dt
                           .AddMinutes(
                               await _bookingService.GetLocationHearingLength(locationId,
                                   HearingType.TMC))
                           .ToString("hh:mm tt"),
                LocationName = await _bookingService.GetLocationName(locationId),
                TypeOfConferenceHearing = "Trial Management Conference",
                ContainerId = containerId,
                LocationId = locationId,
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
            // fake user id for testing without BCeID
            string userId = "B8C1EC79-6464-4C62-BF33-05FC00CC21A0";

            //read user-guid in headers
            if (_httpContext.Request.Headers.ContainsKey("SMGOV-USERGUID"))
            {
                userId = _httpContext.Request.Headers["SMGOV-USERGUID"].ToString();
            }

            int hearingLength =
                await _bookingService.GetLocationHearingLength(model.LocationId, HearingType.TMC);

            //make booking
            return View(
                await _bookingService.BookCourtCase(model, HearingType.TMC, hearingLength, userId));
        }
    }
}
