using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.Booking.MVC.Models;
using SCJ.SC.OnlineBooking;
using System.Linq;
using System.Text.RegularExpressions;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        //API Client
        FakeOnlineBookingClient _client = new FakeOnlineBookingClient();

        //CONST
        const int hearingLength = 30; //30min per session
        const int hearingId = 9010; //Hardcoded for now

        [HttpGet]
        public async Task<IActionResult> CaseSearch()
        {
            //Populate dropdown list values
            return View(await LoadForm());
        }

        [HttpPost]
        public async Task<IActionResult> CaseSearch(CaseSearchViewModel model)
        {
            //get results
            CaseSearchViewModel csvm = await GetResults(model);

            //test if the user selected a timeslot that is available
            if( csvm.ContainerId > 0 && !csvm.TimeslotExpired)
                return RedirectToAction("CaseConfirm", new { caseId = csvm.CaseNumber, locationId = csvm.SelectedRegistryId, containerId = csvm.ContainerId, bookingTime = csvm.SelectedCaseDate }); //go to confirmation screen
            else
                return View(csvm); //return results
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
                CaseNumber = await BuildCaseNumber(caseId, locationId),
                Date = dt.ToString("dddd, MMMM dd, yyyy"),
                Time = dt.ToString("hh:mm tt") + " - " + dt.AddMinutes(hearingLength).ToString("hh:mm tt"),
                LocationName = await GetLocationName(locationId),
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
            return View(await BookCourtCase(model));
        }




        #region Helpers - Needs to move to a services layer

        /// <summary>
        /// Populate the dropdown list for locations for the search
        /// </summary>
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


                    retval.Results = await _client.AvailableDatesByLocationAsync(Convert.ToInt32(model.SelectedRegistryId), hearingId);

                    //set location name
                    SelectListItem selectedRegistry = retval.Registry.FirstOrDefault(x => x.Value == retval.SelectedRegistryId.ToString());

                    if (selectedRegistry != null)
                        retval.SelectedRegistryName = selectedRegistry.Text;

                    //check for valid date
                    if (model.ContainerId > 0)
                    {
                        if (!await IsTimeStillAvailable(model.ContainerId, model.SelectedRegistryId, hearingId))
                            retval.TimeslotExpired = true;

                        //convert JS ticks to .Net date
                        DateTime dt = new DateTime(Convert.ToInt64(model.SelectedCaseDate));

                        //set date properties
                        retval.ContainerId = model.ContainerId;
                        retval.SelectedCaseDate = model.SelectedCaseDate;
                        retval.TimeslotFriendlyName = dt.ToString("MMMM dd") + " from " + dt.ToString("hh:mm tt") + " to " + dt.AddMinutes(hearingLength).ToString("hh:mm tt");
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
        private async Task<string> GetLocationName(int locationId)
        {
            //load all locations
            var locations = await _client.getLocationsAsync();

            //fetch location name
            return locations.Where(x => x.locationID == locationId).FirstOrDefault().locationName;
        }


        /// <summary>
        /// Book court case
        /// </summary>
        private async Task<CaseConfirmViewModel> BookCourtCase(CaseConfirmViewModel model)
        {
            //ensure timeslot is still available
            if (await IsTimeStillAvailable(model.ContainerId, model.LocationId, hearingId))
            {
                //build object to send to the API
                BookHearingInfo bhi = new BookHearingInfo()
                {
                    caseID = Convert.ToInt32(Regex.Replace(model.CaseNumber, "[A-Za-z ]", "")),
                    containerID = model.ContainerId,
                    dateTime = model.FullDate,
                    hearingLength = hearingLength,
                    locationID = model.LocationId,
                    requestedBy = "USER",
                    hearingTypeId = (int)Utils.Enums.ConferenceHearingType.TRIAL_MANAGEMENT_CONFERENCE
                };

                //submit booking
                var result = await _client.BookingHearingAsync(bhi);

                //test to see if the booking was successful
                if (result.bookingResult.ToLower().StartsWith("success"))
                    model.IsBooked = true;
                else
                    model.IsBooked = false;

                return model;
            }
            else
            {
                //The booking is not available anymore
                //user needs to choose a new timeslot
                model.IsTimeslotAvailable = false;
                model.IsBooked = false;

                return model;
            }
        }

        #endregion

    }
}
