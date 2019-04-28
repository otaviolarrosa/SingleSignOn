using System;

namespace SingleSignOn.Caching.Interfaces
{
    public interface ICacheManager
    {
        void SetCache(string key, string value);
        void SetCache(string key, string value, DateTime expirationDate);
        string GetCache(string key);
        void ClearCache(string key);
    }
}
