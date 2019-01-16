using System;
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

        [HttpGet]
        public async Task<IActionResult> CaseConfirm(int caseId, int locationId, int containerId, string bookingTime)
        {
            //Check if timeslot is still available
            if(await IsTimeStillAvailable(containerId, locationId, 9010))
            {
                //convert JS ticks to .Net date
                DateTime dt = new DateTime(Convert.ToInt64(bookingTime));

                //Timeslot is still available
                CaseConfirmViewModel ccm = new CaseConfirmViewModel()
                {
                    CaseNumber = await BuildCaseNumber( caseId, locationId),
                    Date = dt.ToString("dddd, MMMM dd, yyyy"),
                    Time = dt.ToString("hh:mm tt") + " - " + dt.AddMinutes(30).ToString("hh:mm tt"),
                    LocationName = await GetLocationName(locationId),
                    TypeOfConferenceHearing = "Trial Management Conference"
                };

                return View(ccm);

            }
            else
            {

                //TODO:
                //This is not right, will fix this.

                //Timeslot is not available anymore
                //Go back to case-search page

                CaseSearchViewModel csm = new CaseSearchViewModel()
                {
                    CaseNumber = await BuildCaseNumber(caseId, locationId),
                    ConferenceType = Utils.Enums.ConferenceHearingType.TRIAL_MANAGEMENT_CONFERENCE,
                    IsValidCaseNumber = true,
                    NoAvailableTimes = false,
                    TimeslotExpired = true, 
                    SelectedRegistryId = locationId,
                };

                return RedirectToAction("casesearch", csm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CaseBooked(CaseConfirmViewModel model)
        {
            return View(await BookCourtCase(model));
        }




        #region Helpers - Needs to move to a services layer

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
                #region Always set the dropdown values and case number

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

                //keep reference to current searched case number
                retval.CaseNumber = model.CaseNumber;

                //keep timeslot expired value
                retval.TimeslotExpired = model.TimeslotExpired;

                #endregion

                //search the current case number
                if (await _client.caseNumberValidAsync(model.CaseNumber) == 0)
                {
                    //case could not be found
                    retval.IsValidCaseNumber = false;

                    //empty result set
                    retval.Results = new AvailableDatesByLocation();
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


        /// <summary>
        /// Check if a timeslot is still available for a court booking
        /// </summary>
        /// <param name="containerId">This will be linked to the time</param>
        /// <param name="locationId">Location for the booking</param>
        /// <param name="hearingId"></param>
        /// <returns></returns>
        private async Task<bool> IsTimeStillAvailable(int containerId, int locationId, int hearingId)
        {
            //get all locations
            var locationsAvailable = await _client.AvailableDatesByLocationAsync(locationId, hearingId);

            //try and get location for specific container
            var timeslot = locationsAvailable.AvailableDates.Select(x => x.ContainerID == containerId);

            //if we could load container, slot is still available
            return timeslot != null ? true : false;
        }


        /// <summary>
        /// Fetch location-code for specific case ID
        /// </summary>
        /// <param name="caseId">Case ID number</param>
        /// <param name="locationId">Location ID number</param>
        /// <returns></returns>
        private async Task<string> BuildCaseNumber(int caseId, int locationId)
        {
            //set default
            string caseNumber = caseId.ToString();

            //load all locations
            var locations = await _client.getLocationsAsync();

            //fetch location prefix
            var locationPrefix = locations.Where(x => x.locationID == locationId).FirstOrDefault().locationCode;

            //return location prefix + case number
            return locationPrefix + caseNumber;
        }


        /// <summary>
        /// Fetch the location name based on the location ID
        /// </summary>
        /// <param name="locationId">The ID for the location</param>
        /// <returns>Location Name</returns>
        private async Task<string> GetLocationName(int locationId)
        {
            //load all locations
            var locations = await _client.getLocationsAsync();

            //fetch location name
            return locations.Where(x => x.locationID == locationId).FirstOrDefault().locationName;
        }


        private async Task<CaseConfirmViewModel> BookCourtCase(CaseConfirmViewModel model)
        {
            //ensure timeslot is still available
            if (await IsTimeStillAvailable(0, 0, 0))
            {
                return model;
            }
            else
            {
                model.IsTimeslotAvailable = false;
                model.IsBooked = false;

                return model;
            }
        }

        #endregion

    }
}
