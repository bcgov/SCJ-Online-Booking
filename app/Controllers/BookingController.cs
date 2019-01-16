using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.Booking.MVC.Models;
using SCJ.SC.OnlineBooking;
using System.Linq;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        //API Client
        FakeOnlineBookingClient _client = new FakeOnlineBookingClient();

        [HttpGet]
        public async Task<IActionResult> CaseSearch()
        {
            //Populate dropdown list values
            return View(await LoadForm());
        }

        [HttpPost]
        public async Task<IActionResult> CaseSearch(CaseSearchViewModel model)
        {
            return View(await GetResults(model)); //fetch resutls for case number and type
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        #region Helpers

        /// <summary>
        /// Populate the dropdown list for locations for the search
        /// </summary>
        /// <returns></returns>
        private async Task<CaseSearchViewModel> LoadForm()
        {
            //Model instance
            CaseSearchViewModel retval = new CaseSearchViewModel();

            //Load locations from API
            var locationsAsync = await _client.getLocationsAsync();

            //Add "please select" item in the list
            var allLocations = locationsAsync.Prepend(new Location() { locationCode = "", locationID = -1, locationName = "Please select an option" }).ToList();

            //set model property
            retval.Registry = new SelectList(allLocations.Select(x => new { Id = x.locationID, Value = x.locationName }), "Id", "Value");

            //return model to view
            return retval;
        }






        /// <summary>
        /// Search for available times
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<CaseSearchViewModel> GetResults(CaseSearchViewModel model)
        {
            CaseSearchViewModel retval = new CaseSearchViewModel();
            try
            {
                #region Always set the dropdown values

                // Load locations from API
                var locationsAsync = await _client.getLocationsAsync();

                //Add "please select" item in the list
                var allLocations = locationsAsync.Prepend(new Location() { locationCode = "", locationID = -1, locationName = "Please select an option" }).ToList();

                //populate select
                retval.Registry = new SelectList(allLocations.Select(x => new { Id = x.locationID, Value = x.locationName }), "Id", "Value");

                //keep reference to current conference type
                retval.ConferenceType = model.ConferenceType;

                //keep reference to current selected registry
                retval.SelectedRegistryId = model.SelectedRegistryId;

                #endregion

                //search the current case number
                if (await _client.caseNumberValidAsync(model.CaseNumber) == 0)
                {
                    //case could not be found
                    retval.IsValidCaseNumber = false;

                    //empty result set
                    retval.Results = new AvailableDatesByLocation();

                    //keep reference to current searched case number
                    retval.CaseNumber = model.CaseNumber;
                }
                else
                {
                    //valid case number
                    retval.IsValidCaseNumber = true;

                    //TODO:
                    //What is the hearingTypeID?
                    //Default to 1 for now
                    retval.Results = await _client.AvailableDatesByLocationAsync(Convert.ToInt32(model.SelectedRegistryId), 9010);

                    //set location name
                    SelectListItem selectedRegistry = retval.Registry.FirstOrDefault(x => x.Value == retval.SelectedRegistryId.ToString());

                    if (selectedRegistry != null)
                    {
                        retval.SelectedRegistryName = selectedRegistry.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO:
                //Handle exception
            }

            return retval;
        }

        #endregion

    }
}
