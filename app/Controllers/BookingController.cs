using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.SC.OnlineBooking;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;
using SCJ.Booking.MVC.Data;
using Microsoft.AspNetCore.Http;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        //API Client
        private readonly IOnlineBooking _client = OnlineBookingClientFactory.GetClient();

        //Services
        readonly BookingService _bookingService;

        //HttpContext
        private IHttpContextAccessor _httpContextAccessor;

        //DB contect
        private ApplicationDbContext _dbContext;

        //CONST
        const int hearingId = 9010; //Hardcoded for now

        //Environment
        private bool _isDevEnvironment = false;

        //Constructor
        public BookingController(ApplicationDbContext context, IHttpContextAccessor httpAccessor)
        {
            _dbContext = context;
            _httpContextAccessor = httpAccessor;
            _bookingService = new BookingService(_dbContext, _httpContextAccessor);

            //test the environment
            if (Environment.GetEnvironmentVariable("TAG_NAME").ToLower().Equals("localdev"))
                _isDevEnvironment = true;
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
            CaseSearchViewModel csvm = await _bookingService.GetResults(model, _client, hearingId, await _bookingService.GetLocationHearingLength(model.SelectedRegistryId, hearingId, _client));

            //test if the user selected a timeslot that is available
            if (csvm != null && csvm.ContainerId > 0 && !csvm.TimeslotExpired)
                //go to confirmation screen
                return RedirectToAction("CaseConfirm", new { caseId = csvm.CaseNumber, locationId = csvm.SelectedRegistryId, containerId = csvm.ContainerId, bookingTime = csvm.SelectedCaseDate });
            else
                //return results (User have not selected a date, or the date is not available anymore)
                return View(csvm);
        }

        [HttpGet]
        public async Task<IActionResult> CaseConfirm(string caseId, int locationId, int containerId, string bookingTime)
        {
            //convert JS ticks to .Net date
            DateTime dt = new DateTime(Convert.ToInt64(bookingTime));

            //Timeslot is still available
            CaseConfirmViewModel ccm = new CaseConfirmViewModel()
            {
                CaseNumber = await _bookingService.BuildCaseNumber(caseId, locationId, _client),
                Date = dt.ToString("dddd, MMMM dd, yyyy"),
                Time = dt.ToString("hh:mm tt") + " - " + dt.AddMinutes(await _bookingService.GetLocationHearingLength(locationId, hearingId, _client)).ToString("hh:mm tt"),
                LocationName = await _bookingService.GetLocationName(locationId, _client),
                TypeOfConferenceHearing = "Trial Management Conference",
                ContainerId = containerId,
                LocationId = locationId,
                FullDate = dt,
                IsUserKnown = true
            };

            return View(ccm);
        }

        [HttpPost]
        public async Task<IActionResult> CaseBooked(CaseConfirmViewModel model)
        {
            string userId = string.Empty;

            if (!_isDevEnvironment)
            {
                //read user-guid in headers
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("SMGOV-USERGUID"))
                    userId = _httpContextAccessor.HttpContext.Request.Headers["SMGOV-USERGUID"].ToString();
            }
            else
            {
                //Dummy user guid
                userId = "B8C1EC79-6464-4C62-BF33-05FC00CC21A0";
            }

            //make booking
            return View(await _bookingService.BookCourtCase(model, _client, hearingId, await _bookingService.GetLocationHearingLength(model.LocationId, hearingId, _client), userId));
        }

    }
}
