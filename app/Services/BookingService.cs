using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.SC.OnlineBooking;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SCJ.Booking.MVC.ViewModels;
using Serilog;
using SCJ.Booking.MVC.Data;
using SCJ.Booking.MVC.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;

namespace SCJ.Booking.MVC.Services
{
    public class BookingService
    {
        //Init error logger
        Serilog.Core.Logger _logger = null;
        private readonly ApplicationDbContext _dbContext;

        //HttpContext
        private IHttpContextAccessor _httpContextAccessor;

        //Const
        private const int _maxHearingsPerDay = 3;

        //Environment
        private bool _isLocallDevEnvironment = false;

        //Constructor
        public BookingService(ApplicationDbContext dbContext, IHttpContextAccessor httpAccessor)
        {
            //setup error logger settings
            _logger = new LoggerConfiguration()
            .WriteTo.Console(Serilog.Events.LogEventLevel.Error)
            .CreateLogger();

            //DB Contect setup
            _dbContext = dbContext;

            //HttpContext
            _httpContextAccessor = httpAccessor;

            //test the environment
            if (Environment.GetEnvironmentVariable("TAG_NAME").ToLower().Equals("localdev"))
                _isLocallDevEnvironment = true;
        }


        /// <summary>
        /// Populate the dropdown list for locations for the search
        /// </summary>
        public async Task<CaseSearchViewModel> LoadForm(IOnlineBooking client)
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: LoadForm().");
            }

            //return model to view
            return retval;
        }


        /// <summary>
        /// Search for available times
        /// </summary>
        public async Task<CaseSearchViewModel> GetResults(CaseSearchViewModel model, IOnlineBooking client, int hearingId, int hearingLength)
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
                if (await client.caseNumberValidAsync(await BuildCaseNumber(model.CaseNumber, model.SelectedRegistryId, client)) == 0)
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
        public async Task<bool> IsTimeStillAvailable(int containerId, int locationId, int hearingId, IOnlineBooking client)
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: IsTimeStillAvailable().");
            }

            return isTimeAvailable;
        }


        /// <summary>
        /// Fetch location-code for specific case ID
        /// </summary>
        public async Task<string> BuildCaseNumber(string caseId, int locationId, IOnlineBooking client)
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: BuildCaseNumber().");
            }

            //return location prefix + case number
            return locationPrefix + caseId.ToString();
        }


        /// <summary>
        /// Fetch the location name based on the location ID
        /// </summary>
        public async Task<string> GetLocationName(int locationId, IOnlineBooking client)
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: GetLocationName().");
            }

            //fetch location name
            return locationName;
        }


        /// <summary>
        /// Fetch the location name based on the location ID
        /// </summary>
        public async Task<int> GetLocationHearingLength(int locationId, int hearingTypeId, IOnlineBooking client)
        {
            //default value
            int locationLength = 0;

            try
            {
                //load all locations
                var availableDatesByLocation = await client.AvailableDatesByLocationAsync(locationId, hearingTypeId);

                //set location length
                locationLength = availableDatesByLocation.BookingDetails.detailBookingLength;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: GetLocationHearingLength().");
            }

            //fetch location length
            return locationLength;
        }


        /// <summary>
        /// Book court case
        /// </summary>
        public async Task<CaseConfirmViewModel> BookCourtCase(CaseConfirmViewModel model, IOnlineBooking client, int hearingId, int hearingLength, string userId)
        {
            try
            {
                //if the user could not be detected return 
                if (String.IsNullOrWhiteSpace(userId))
                {
                    model.IsUserKnown = false;
                    return model;
                }

                //we know who the user is.
                model.IsUserKnown = true;

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
                    {
                        //create database entry
                        var bookingInfo = _dbContext.Set<BookingHistory>();

                        bookingInfo.Add(new BookingHistory { ContainerId = model.ContainerId, SmGovUserGuid = userId, Timestamp = DateTime.Now });

                        //save to DB
                        _dbContext.SaveChanges();

                        //update model
                        model.IsBooked = true;
                    }
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in service. Metod: BookCourtCase().");
            }

            return model;
        }


        /// <summary>
        /// Get the number of hearings left for the day
        /// </summary>
        /// <returns></returns>
        public HtmlString GetHearingsRemaining()
        {
            return new HtmlString(GetUserHearingsTotalRemaining().ToString());
        }


        /// <summary>
        /// Read the database and get the total number of hearings left for the day
        /// </summary>
        /// <returns></returns>
        private int GetUserHearingsTotalRemaining()
        {
            //get user GUID
            var uGuid = string.Empty;

            if (!_isLocallDevEnvironment)
            {
                //try and read the header
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("SMGOV-USERGUID"))
                    uGuid = _httpContextAccessor.HttpContext.Request.Headers["SMGOV-USERGUID"].ToString();
                else return 0;
            }
            else
            {
                //Dummy user guid
                uGuid = "B8C1EC79-6464-4C62-BF33-05FC00CC21A0"; 
            }

            //today's date
            var todaysDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            //get all entries for logged-in user
            //booked on today
            var hearingsBookedForToday = _dbContext.BookingHistory.Where(b => b.SmGovUserGuid == uGuid && b.Timestamp.Day == todaysDate.Day && b.Timestamp.Month == todaysDate.Month && b.Timestamp.Year == b.Timestamp.Year).ToList();

            if (hearingsBookedForToday != null && hearingsBookedForToday.Count() > 0)
                //return number of hearings booked for today minus the max bookings allowed
                return (_maxHearingsPerDay - hearingsBookedForToday.Count());
            else
                //User have not made any bookings for the day
                return _maxHearingsPerDay;
        }
    }
}