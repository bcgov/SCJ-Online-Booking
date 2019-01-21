using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.SC.OnlineBooking;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        //API Client
        private readonly IOnlineBooking _client = OnlineBookingClientFactory.GetClient(true);

        //Services
        readonly BookingService _bookingService = new BookingService();

        //CONST
        const int hearingLength = 30; //30min per session
        const int hearingId = 9010; //Hardcoded for now

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
            CaseSearchViewModel csvm = await _bookingService.GetResults(model, _client, hearingId, hearingLength);

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
                Time = dt.ToString("hh:mm tt") + " - " + dt.AddMinutes(hearingLength).ToString("hh:mm tt"),
                LocationName = await _bookingService.GetLocationName(locationId, _client),
                TypeOfConferenceHearing = "Trial Management Conference",
                ContainerId = containerId,
                LocationId = locationId,
                FullDate = dt
            };

            return View(ccm);
        }

        [HttpPost]
        public async Task<IActionResult> CaseBooked(CaseConfirmViewModel model)
        {
            return View(await _bookingService.BookCourtCase(model, _client, hearingId, hearingLength));
        }

    }
}
