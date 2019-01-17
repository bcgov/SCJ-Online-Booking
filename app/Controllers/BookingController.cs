using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Models;
using SCJ.SC.OnlineBooking;
using System.Text.RegularExpressions;
using SCJ.Booking.MVC.Services;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        //API Client
        FakeOnlineBookingClient _client = new FakeOnlineBookingClient();

        //Services
        BookingService _bookingService = new BookingService();

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
            if (csvm.ContainerId > 0 && !csvm.TimeslotExpired)
                //go to confirmation screen
                return RedirectToAction("CaseConfirm", new { caseId = Regex.Replace(csvm.CaseNumber, "[A-Za-z ]", ""), locationId = csvm.SelectedRegistryId, containerId = csvm.ContainerId, bookingTime = csvm.SelectedCaseDate }); 
            else
                //return results (User have not selected a date, or the date is not available anymore)
                return View(csvm); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> CaseConfirm(int caseId, int locationId, int containerId, string bookingTime)
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
