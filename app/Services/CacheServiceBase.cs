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
        protected const int CacheSlidingExpirySeconds = 1200; // 20 minutes
        protected const int CacheAbsoluteExpirySeconds = 3600; // 1 hour
        protected readonly IDistributedCache Cache;

        protected CacheServiceBase(IDistributedCache cache)
        {
            Cache = cache;
        }

        /// <summary>
        ///     Checks if a key exists in the cache
        /// </summary>
        protected bool Exists(string cacheKey)
        {
            return Cache.Get(cacheKey) != null;
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
        protected T GetObject<T>(string cacheKey)
        {
            byte[] b = Cache.Get(cacheKey);

            if (b == null)
            {
                // ReSharper disable once RedundantTypeSpecificationInDefaultExpression
                return default;
            }

            return JsonConvert.DeserializeObject<T>(Encoding.Unicode.GetString(b));
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
                return default;
            }

            return JsonConvert.DeserializeObject<T>(Encoding.Unicode.GetString(b));
        }

        protected void SaveString(
            string cacheKey,
            string value,
            int slidingExpirySeconds = CacheSlidingExpirySeconds,
            int absoluteExpirySeconds = CacheAbsoluteExpirySeconds
        )
        {
            Remove(cacheKey);

            byte[] b = Encoding.Unicode.GetBytes(value);

            Cache.Set(
                cacheKey,
                b,
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(slidingExpirySeconds),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpirySeconds)
                }
            );
        }

        /// <summary>
        ///     Saves a string to the cache
        /// </summary>
        protected async Task SaveStringAsync(
            string cacheKey,
            string value,
            int slidingExpirySeconds = CacheSlidingExpirySeconds,
            int absoluteExpirySeconds = CacheAbsoluteExpirySeconds
        )
        {
            await RemoveAsync(cacheKey);

            byte[] b = Encoding.Unicode.GetBytes(value);

            await Cache.SetAsync(
                cacheKey,
                b,
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(slidingExpirySeconds),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpirySeconds)
                }
            );
        }

        /// <summary>
        ///     Saves an object to the cache
        /// </summary>
        protected void SaveObject(string cacheKey, object value, int slidingExpirySeconds = 600)
        {
            SaveString(cacheKey, JsonConvert.SerializeObject(value), slidingExpirySeconds);
        }

        /// <summary>
        ///     Saves an object to the cache
        /// </summary>
        protected async Task SaveObjectAsync(
            string cacheKey,
            object value,
            int slidingExpirySeconds = 600
        )
        {
            await SaveStringAsync(
                cacheKey,
                JsonConvert.SerializeObject(value),
                slidingExpirySeconds
            );
        }

        /// <summary>
        ///     Removes an item from the cache
        /// </summary>
        protected void Remove(string cacheKey)
        {
            if (Cache.Get(cacheKey) != null)
            {
                Cache.Remove(cacheKey);
            }
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
