using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCJ.Booking.MVC.Services
{
    public class CoaCacheService : CacheServiceBase
    {
        private const string CoaChambersApplicationTypesKey = "COA_CHAMBERS_APPLICATION_TYPES";

        // services
        private readonly IConfiguration _configuration;

        public CoaCacheService(IDistributedCache cache, IConfiguration configuration) : base(cache)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Gets the list of Chambers Application Types
        /// </summary>
        public async Task<CoAChambersApplications[]> GetChambersApplicationTypesAsync(string caseType)
        {
            if (await ExistsAsync(CoaChambersApplicationTypesKey))
            {
                return await GetObjectAsync<CoAChambersApplications[]>(CoaChambersApplicationTypesKey);
            }

            IOnlineBooking client = OnlineBookingClientFactory.GetClient(_configuration);

            CoAChambersApplications[] applicationTypes = await client.CoAChambersApplicationsListAsync(caseType);

            await SaveObjectAsync(CoaChambersApplicationTypesKey, applicationTypes, CacheSlidingExpirySeconds);

            return applicationTypes;
        }
    }
}
