using Microsoft.Extensions.Caching.Distributed;
using SingleSignOn.Caching.Interfaces;
using System;

namespace SingleSignOn.Caching
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache distributedCache;

        public CacheManager(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public void SetCache(string key, string value)
        {
            distributedCache.SetString(key, value);
        }

        public void SetCache(string key, string value, DateTime expirationDate)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30)
            };
            distributedCache.SetString(key, value, cacheOptions);
        }

        public string GetCache(string key)
        {
            return distributedCache.GetString(key);
        }

        public void ClearCache(string key)
        {
            distributedCache.Remove(key);
        }
    }
}
