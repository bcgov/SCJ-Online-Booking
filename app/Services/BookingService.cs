using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.Booking.MVC.Models;
using SCJ.SC.OnlineBooking;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace SCJ.Booking.MVC.Services
{
    public class BookingService
    {
        //Init error logger
        Serilog.Core.Logger _logger = null;

        //Constructor
        public BookingService()
        {
            //setup error logger settings
            _logger = new LoggerConfiguration()
            .WriteTo.Console(Serilog.Events.LogEventLevel.Error)
            .CreateLogger();
        }


        /// <summary>
        /// Populate the dropdown list for locations for the search
        /// </summary>
        public async Task<CaseSearchViewModel> LoadForm(FakeOnlineBookingClient client)
        {
            //Model instance
            CaseSearchViewModel retval = new CaseSearchViewModel();
            
            try
            {
                //Load locations from API
                var locationsAsync = await client.getLocationsAsync();

                //set model property
                retval.Registry = new SelectList(locationsAsync.Select(x => new { Id = x.locationID, Value = x.locationName }), "Id", "Value");
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: LoadForm().");
            }

            //return model to view
            return retval;
        }


        /// <summary>
        /// Search for available times
        /// </summary>
        public async Task<CaseSearchViewModel> GetResults(CaseSearchViewModel model, FakeOnlineBookingClient client, int hearingId, int hearingLength)
        {
            CaseSearchViewModel retval = new CaseSearchViewModel();

            try
            {
                #region Always set the dropdown values and case number

                // Load locations from API
                var locationsAsync = await client.getLocationsAsync();

                //populate select
                retval.Registry = new SelectList(locationsAsync.Select(x => new { Id = x.locationID, Value = x.locationName }), "Id", "Value");

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
                if (await client.caseNumberValidAsync(model.CaseNumber) == 0)
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

                    retval.Results = await client.AvailableDatesByLocationAsync(Convert.ToInt32(model.SelectedRegistryId), hearingId);

                    //set location name
                    SelectListItem selectedRegistry = retval.Registry.FirstOrDefault(x => x.Value == retval.SelectedRegistryId.ToString());

                    if (selectedRegistry != null)
                        retval.SelectedRegistryName = selectedRegistry.Text;

                    //check for valid date
                    if (model.ContainerId > 0)
                    {
                        if (!await IsTimeStillAvailable(model.ContainerId, model.SelectedRegistryId, hearingId, client))
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
                _logger.Error(ex, "Error in service. Metod: GetResults().");
            }

            return retval;
        }


        /// <summary>
        /// Check if a timeslot is still available for a court booking
        /// </summary>
        public async Task<bool> IsTimeStillAvailable(int containerId, int locationId, int hearingId, FakeOnlineBookingClient client)
        {
            //default value
            bool isTimeAvailable = false;

            try
            {
                //get all locations
                var locationsAvailable = await client.AvailableDatesByLocationAsync(locationId, hearingId);

                //try and get location for specific container
                var timeslot = locationsAvailable.AvailableDates.Select(x => x.ContainerID == containerId);

                //set return value
                isTimeAvailable = timeslot != null ? true : false;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: IsTimeStillAvailable().");
            }

            return isTimeAvailable;
        }


        /// <summary>
        /// Fetch location-code for specific case ID
        /// </summary>
        public async Task<string> BuildCaseNumber(int caseId, int locationId, FakeOnlineBookingClient client)
        {
            //default value
            string locationPrefix = string.Empty;

            try
            {
                //load all locations
                var locations = await client.getLocationsAsync();

                //fetch location prefix
                locationPrefix = locations.Where(x => x.locationID == locationId).FirstOrDefault().locationCode;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: BuildCaseNumber().");
            }

            //return location prefix + case number
            return locationPrefix + caseId.ToString();
        }


        /// <summary>
        /// Fetch the location name based on the location ID
        /// </summary>
        public async Task<string> GetLocationName(int locationId, FakeOnlineBookingClient client)
        {
            //default value
            string locationName = string.Empty;

            try
            {
                //load all locations
                var locations = await client.getLocationsAsync();

                //set location name
                locationName = locations.Where(x => x.locationID == locationId).FirstOrDefault().locationName;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: GetLocationName().");
            }

            //fetch location name
            return locationName;
        }


        /// <summary>
        /// Book court case
        /// </summary>
        public async Task<CaseConfirmViewModel> BookCourtCase(CaseConfirmViewModel model, FakeOnlineBookingClient client, int hearingId, int hearingLength)
        {
            try
            {
                //ensure timeslot is still available
                if (await IsTimeStillAvailable(model.ContainerId, model.LocationId, hearingId, client))
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
                    var result = await client.BookingHearingAsync(bhi);

                    //test to see if the booking was successful
                    if (result.bookingResult.ToLower().StartsWith("success"))
                        model.IsBooked = true;
                    else
                        model.IsBooked = false;
                }
                else
                {
                    //The booking is not available anymore
                    //user needs to choose a new timeslot
                    model.IsTimeslotAvailable = false;
                    model.IsBooked = false;
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: BookCourtCase().");
            }

            return model;
        }
    }
}
