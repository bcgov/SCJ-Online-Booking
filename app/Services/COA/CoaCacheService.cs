using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.RemoteAPIs;
using SCJ.Booking.TaskRunner.Utils;
using SCJ.OnlineBooking;
using Serilog;

namespace SCJ.Booking.MVC.Services.COA
{
    public class CacheService : CacheServiceBase
    {
        private const string CoaChambersApplicationTypesKey = "COA_CHAMBERS_APPLICATION_TYPES";

        // services
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CacheService(IDistributedCache cache, IConfiguration configuration)
            : base(cache)
        {
            _configuration = configuration;
            _logger = LogHelper.GetLogger(configuration);
        }

        /// <summary>
        ///     Gets the list of Chambers Application Types
        /// </summary>
        public async Task<CoAChambersApplications[]> GetChambersApplicationTypesAsync(
            string caseType
        )
        {
            var cacheKey = $"{CoaChambersApplicationTypesKey}__{caseType}";

            if (await ExistsAsync(cacheKey))
            {
                return await GetObjectAsync<CoAChambersApplications[]>(cacheKey);
            }

            IOnlineBooking client = OnlineBookingClientFactory.GetClient(_configuration);

            _logger.Information($"Calling CoAChambersApplicationsListAsync(\"{caseType}\")");

            CoAChambersApplications[] applicationTypes = await client.coaCHApplicationListAsync(
                caseType
            );

            if (applicationTypes.Length > 0)
            {
                await SaveObjectAsync(cacheKey, applicationTypes);
            }

            _logger.Information(
                $"{applicationTypes.Length} chambers application types were retrieved for '{caseType}'"
            );

            return applicationTypes;
        }
    }
}
