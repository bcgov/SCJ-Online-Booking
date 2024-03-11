using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Services
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
        public async Task<int?> GetBookingLocationIdAsync(int caseLocationId, int hearingTypeId)
        {
            return (await GetLocationsAsync())
                .FirstOrDefault(l =>
                    l.bookingHearingTypeID == hearingTypeId && l.locationID == caseLocationId
                )
                ?.bookingLocationID;
        }

        /// <summary>
        ///     Gets the name of a Supreme Court location based on the id
        /// </summary>
        public async Task<string> GetLocationNameAsync(int locationId)
        {
            return (await GetLocationAsync(locationId))?.locationName ?? "";
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
        public async Task<Dictionary<int, string>> GetLocationDictionaryAsync()
        {
            if (await ExistsAsync(ScRegistryDropdownKey))
            {
                return await GetObjectAsync<Dictionary<int, string>>(ScRegistryDropdownKey);
            }

            Dictionary<int, string> locationList = (await GetLocationsAsync())
                .Select(x => new { x.locationID, x.locationName })
                .Distinct()
                .OrderBy(x => x.locationName)
                .ToDictionary(x => x.locationID, x => x.locationName);

            await SaveObjectAsync(ScRegistryDropdownKey, locationList, CacheSlidingExpirySeconds);

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

            Location[] locations = await client.getLocationsAsync();

            await SaveObjectAsync(ScLocationInfoKey, locations, CacheSlidingExpirySeconds);

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

            await SaveObjectAsync(ScAvailableBookingTypes, bookingTypes, CacheSlidingExpirySeconds);

            return bookingTypes;
        }

        /// <summary>
        ///     Gets a cached list of trial booking formulas
        /// </summary>
        public async Task<FormulaLocation[]> AvailableTrialBookingFormulasByLocationAsync()
        {
            if (await ExistsAsync(ScAvailableBookingFormulas))
            {
                return await GetObjectAsync<FormulaLocation[]>(ScAvailableBookingFormulas);
            }

            var client = OnlineBookingClientFactory.GetClient(_configuration);

            var formulas = await client.AvailableTrialBookingFormulasByLocationAsync("", "");

            await SaveObjectAsync(ScAvailableBookingFormulas, formulas, CacheSlidingExpirySeconds);

            return formulas;
        }
    }
}
