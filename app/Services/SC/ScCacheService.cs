using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Services.SC
{
    /// <summary>
    ///     Cache helper for speeding up database queries
    /// </summary>
    public class ScCacheService : CacheServiceBase
    {
        private const string ScLocationInfoKey = "SC_LOCATION_INFO";
        private const string ScRegistryDropdownKey = "SC_REGISTRY_DROPDOWN";
        private const string ScAvailableBookingTypes = "SC_AVAILABLE_BOOKING_TYPES";
        private const string ScAvailableBookingFormulas = "SC_AVAILABLE_BOOKING_FORMULAS";

        // services
        private readonly IConfiguration _configuration;

        public ScCacheService(IDistributedCache cache, IConfiguration configuration)
            : base(cache)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Gets the booking location for a specified hearingTypeId and locationId
        /// </summary>
        public async Task<int?> GetBookingLocationIdAsync(int locationId, int hearingTypeId)
        {
            if (hearingTypeId == ScHearingType.TRIAL)
            {
                // trials get their booking location id from AvailableTrialBookingFormulasByLocationAsync()
                return null;
            }

            return (await GetLocationsAsync())
                .FirstOrDefault(l =>
                    l.bookingHearingTypeID == hearingTypeId && l.locationID == locationId
                )
                ?.bookingLocationID;
        }

        /// <summary>
        ///     Gets the name of a Supreme Court location based on the id
        /// </summary>
        public async Task<string> GetLocationNameAsync(int locationId)
        {
            return locationId != -1 ? (await GetLocationAsync(locationId)).locationName : null;
        }

        /// <summary>
        ///     Gets a single Supreme Court location based on the id (does not include booking info)
        /// </summary>
        public async Task<Location> GetLocationAsync(int locationId)
        {
            Location[] locations = await GetLocationsAsync();

            Location location = locations.FirstOrDefault(l => l.locationID == locationId);

            if (location == null)
            {
                return null;
            }

            return new Location
            {
                locationID = locationId,
                locationName = location.locationName,
                locationCode = location.locationCode
            };
        }

        /// <summary>
        ///     Gets the list of Supreme Court locations as a dictionary
        /// </summary>
        public Dictionary<int, string> GetLocationDictionary()
        {
            if (Exists(ScRegistryDropdownKey))
            {
                return GetObject<Dictionary<int, string>>(ScRegistryDropdownKey);
            }

            Dictionary<int, string> locationList = GetLocationsAsync()
                .Result.Select(x => new { x.locationID, x.locationName })
                .Distinct()
                .OrderBy(x => x.locationName)
                .ToDictionary(x => x.locationID, x => x.locationName);

            SaveObject(ScRegistryDropdownKey, locationList);

            return locationList;
        }

        /// <summary>
        ///     Gets a cached list of Supreme Court locations
        /// </summary>
        public async Task<Location[]> GetLocationsAsync()
        {
            if (await ExistsAsync(ScLocationInfoKey))
            {
                return await GetObjectAsync<Location[]>(ScLocationInfoKey);
            }

            IOnlineBooking client = OnlineBookingClientFactory.GetClient(_configuration);

            Location[] locations = await client.scGetLocationsAsync();

            await SaveObjectAsync(ScLocationInfoKey, locations);

            return locations;
        }

        /// <summary>
        ///     Gets a cached list of Supreme Court booking types
        /// </summary>
        public async Task<string[]> GetAvailableBookingTypesAsync()
        {
            if (await ExistsAsync(ScAvailableBookingTypes))
            {
                return await GetObjectAsync<string[]>(ScAvailableBookingTypes);
            }

            IOnlineBooking client = OnlineBookingClientFactory.GetClient(_configuration);

            string[] bookingTypes = await client.GetAvailableBookingTypesAsync();

            await SaveObjectAsync(ScAvailableBookingTypes, bookingTypes);

            return bookingTypes;
        }

        /// <summary>
        ///     Gets a cached list of trial booking formula locations
        /// </summary>
        public async Task<FormulaLocation[]> AvailableTrialBookingFormulasByLocationAsync()
        {
            if (await ExistsAsync(ScAvailableBookingFormulas))
            {
                return await GetObjectAsync<FormulaLocation[]>(ScAvailableBookingFormulas);
            }

            var client = OnlineBookingClientFactory.GetClient(_configuration);

            var formulas = await client.scAvailableFormulasByHearingTypeAndLocationAsync("", "");

            await SaveObjectAsync(ScAvailableBookingFormulas, formulas);

            return formulas;
        }

        /// <summary>
        ///     Gets a trial booking formula location for a specified court class
        /// </summary>
        public async Task<FormulaLocation> GetFormulaLocationAsync(
            string formulaType,
            int locationId,
            string courtClass
        )
        {
            var formulas = await AvailableTrialBookingFormulasByLocationAsync();

            // look for a special formula location for the specific courtClass
            var result = formulas.FirstOrDefault(f =>
                f.FormulaType == formulaType
                && f.LocationID == locationId
                && f.BookingHearingCode == courtClass
            );

            if (result != null)
            {
                return result;
            }

            // if there isn't a special formula location then use the general one
            // family (E) doesn't get include in "All Other"
            var all = courtClass == "E" ? new[] { "All" } : new[] { "All", "All Other" };

            return formulas.FirstOrDefault(f =>
                f.FormulaType == formulaType
                && f.LocationID == locationId
                && all.Contains(f.BookingHearingCode)
            );
        }

        /// <summary>
        ///     Gets the list of chambers hearing sub types
        /// </summary>
        public Dictionary<int, string> GetChambersHearingSubTypes()
        {
            // TODO: this needs to use the API method when it's available
            return new Dictionary<int, string>
            {
                { 9012, "Chambers (default or other)" },
                { 9020, "Chambers Judicial Review" },
                { 9022, "Chambers Petition" },
                { 9013, "Chambers Summary Trial" },
                { 9014, "Chambers Appeal from Associate Judge or Registrar" },
                { 9010, "Appeal from Provincial Court" }
            };
        }
    }
}
