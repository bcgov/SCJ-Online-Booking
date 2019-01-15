using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.Booking.MVC.Models;
using SCJ.Booking.RemoteAPIs.Fixtures;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        FakeOnlineBookingClient _client = new FakeOnlineBookingClient();

        public IActionResult BookYourHearing()
        {
            return View(LoadForm());
        }

        //search on initial search page
        public IActionResult Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
                return View("Results", GetResults(model)); //fetch resutls for case number and type
            else
                return View("BookYourHearing", new SearchViewModel()); //invalid model
        }

        //search on results page
        public IActionResult SearchResults(SearchResultsViewModel model)
        {
            SearchViewModel svm = new SearchViewModel()
            {
                CaseNumber = model.CaseNumber,
                ConferenceType = model.ConferenceType
            };

            return View("Results", GetResults(svm));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<SearchResultsViewModel> GetResults(SearchViewModel model)
        {
            SearchResultsViewModel retval = new SearchResultsViewModel();
            try
            {
                if (await _client.caseNumberValidAsync(model.CaseNumber) == 0)
                {
                    retval.IsValidCaseNumber = false;
                    retval.Results = new System.Collections.Generic.List<AvailableDatesByLocation>();
                    retval.CaseNumber = model.CaseNumber;
                    retval.ConferenceType = model.ConferenceType;
                }
            }
            catch(Exception ex)
            {
                //TODO:
                //Handle exception
            }

            return retval;
        }

        private SearchViewModel LoadForm()
        {
            var lstRegistryItem1 = new SelectListItem() { Text = "Vancouver", Value = "10" };
            var lstRegistryItem2 = new SelectListItem() { Text = "West Vancouver", Value = "9" };
            var lstRegistryItem3 = new SelectListItem() { Text = "East Vancouver", Value = "8" };

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(lstRegistryItem1);
            items.Add(lstRegistryItem2);
            items.Add(lstRegistryItem3);

            SearchViewModel retval = new SearchViewModel()
            {
                Registry = items,
            };

            //var locations = _client.getLocationsAsync().Result;

            //locations.Select(x =>
            //            new SelectListItem
            //            {
            //                Value = x.UserRoleId.ToString(),
            //                Text = x.UserRole
            //            });



            return retval;
        }
    }
}
