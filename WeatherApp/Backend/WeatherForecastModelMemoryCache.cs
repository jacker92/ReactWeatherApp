using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace WeatherApp.Backend
{
    public class WeatherForecastModelMemoryCache<TItem> : IMemoryCache<TItem>
    {
        private readonly MemoryCache _cache; 

        public WeatherForecastModelMemoryCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = 1024
            });
        }

        public IEnumerable<TItem> GetOrCreate(object key, Func<IEnumerable<TItem>> createItem)
        {
            if (!_cache.TryGetValue(key, out IEnumerable<TItem> cacheEntry))// Look for cache key.
            {
                // Key not in cache, so get data.
                cacheEntry = createItem();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                 .SetSize(1)//Size amount
                            //Priority on removing when reaching size limit (memory pressure)
                    .SetPriority(CacheItemPriority.High)
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    // Remove from cache after this time, regardless of sliding expiration
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                // Save data in cache.
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
    }
}
