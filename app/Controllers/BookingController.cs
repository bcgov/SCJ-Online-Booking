using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.MVC.Data;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        //CONST
        private const int HearingId = 9010; //Hardcoded for now

        //Services
        private readonly BookingService _bookingService;

        //API Client
        private readonly IOnlineBooking _client;

        //HttpContext
        private readonly HttpContext _httpContext;

        //Environment
        private readonly bool _isLocalDevEnvironment;

        //Constructor
        public BookingController(ApplicationDbContext context, IHttpContextAccessor httpAccessor,
            IConfiguration configuration)
        {
            _httpContext = httpAccessor.HttpContext;
            _client = OnlineBookingClientFactory.GetClient(configuration);
            _bookingService = new BookingService(context, httpAccessor);

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
            return View(await _bookingService.LoadForm(_client));
        }

        [HttpPost]
        public async Task<IActionResult> CaseSearch(CaseSearchViewModel model)
        {
            //get results from services layer. 
            CaseSearchViewModel csvm = await _bookingService.GetResults(model, _client, HearingId,
                await _bookingService.GetLocationHearingLength(model.SelectedRegistryId, HearingId,
                    _client));

            //test if the user selected a timeslot that is available
            if (csvm != null && csvm.ContainerId > 0 && !csvm.TimeSlotExpired)
                //go to confirmation screen
            {
                return RedirectToAction("CaseConfirm",
                    new
                    {
                        caseId = csvm.CaseNumber, locationId = csvm.SelectedRegistryId,
                        containerId = csvm.ContainerId, bookingTime = csvm.SelectedCaseDate
                    });
            }

            return View(csvm);
        }

        [HttpGet]
        public async Task<IActionResult> CaseConfirm(string caseId, int locationId, int containerId,
            string bookingTime)
        {
            //convert JS ticks to .Net date
            var dt = new DateTime(Convert.ToInt64(bookingTime));

            //Timeslot is still available
            var ccm = new CaseConfirmViewModel
            {
                CaseNumber = caseId,
                Date = dt.ToString("dddd, MMMM dd, yyyy"),
                Time = dt.ToString("hh:mm tt") + " - " + dt
                           .AddMinutes(
                               await _bookingService.GetLocationHearingLength(locationId, HearingId,
                                   _client)).ToString("hh:mm tt"),
                LocationName = await _bookingService.GetLocationName(locationId, _client),
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
            string userId = string.Empty;

            if (!_isLocalDevEnvironment)
            {
                //read user-guid in headers
                if (_httpContext.Request.Headers.ContainsKey("SMGOV-USERGUID"))
                {
                    userId = _httpContext.Request.Headers["SMGOV-USERGUID"].ToString();
                }
                else
                {
                    //Dummy user guid
                    userId = "B8C1EC79-6464-4C62-BF33-05FC00CC21A0";
                }
            }

            int hearingLength =
                await _bookingService.GetLocationHearingLength(model.LocationId, HearingId,
                    _client);

            //make booking
            return View(await _bookingService.BookCourtCase(model, _client, HearingId,
                hearingLength, userId));
        }
    }
}
