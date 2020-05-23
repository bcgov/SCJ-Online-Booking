using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace SCJ.Booking.MVC.Services
{
    /// <summary>
    ///     Cache helper for speeding up database queries
    /// </summary>
    public abstract class CacheServiceBase
    {
        protected const int CacheSlidingExpirySeconds = 3600; // 1 hour
        protected readonly IDistributedCache Cache;

        protected CacheServiceBase(IDistributedCache cache)
        {
            Cache = cache;
        }

        /// <summary>
        ///     Checks if a key exists in the cache
        /// </summary>
        protected async Task<bool> ExistsAsync(string cacheKey)
        {
            return await Cache.GetAsync(cacheKey) != null;
        }

        /// <summary>
        ///     Gets an object from the cache
        /// </summary>
        protected async Task<T> GetObjectAsync<T>(string cacheKey)
        {
            byte[] b = await Cache.GetAsync(cacheKey);

            if (b == null)
            {
                // ReSharper disable once RedundantTypeSpecificationInDefaultExpression
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(Encoding.Unicode.GetString(b));
        }

        /// <summary>
        ///     Saves a string to the cache
        /// </summary>
        protected async Task SaveStringAsync(string cacheKey, string value,
            int slidingExpirySeconds = 600)
        {
            await RemoveAsync(cacheKey);

            byte[] b = Encoding.Unicode.GetBytes(value);

            await Cache.SetAsync(cacheKey, b, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpirySeconds)
            });
        }

        /// <summary>
        ///     Saves an object to the cache
        /// </summary>
        protected async Task SaveObjectAsync(string cacheKey, object value,
            int slidingExpirySeconds = 600)
        {
            await SaveStringAsync(cacheKey, JsonConvert.SerializeObject(value),
                slidingExpirySeconds);
        }

        /// <summary>
        ///     Removes an item from the cache
        /// </summary>
        protected async Task RemoveAsync(string cacheKey)
        {
            if (await Cache.GetAsync(cacheKey) != null)
            {
                await Cache.RemoveAsync(cacheKey);
            }
        }
    }
}
