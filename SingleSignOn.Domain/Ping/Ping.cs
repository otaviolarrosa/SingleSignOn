using SingleSignOn.Caching.Interfaces;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Enums.Ping;
using SingleSignOn.Domain.Interfaces.Ping;
using SingleSignOn.Domain.ViewModels.Ping;
using SingleSignOn.Utils.ExtensionMethods;
using System;

namespace SingleSignOn.Domain.Ping
{
    public class Ping : IPing
    {
        private readonly IPingRepository pingRepository;
        private readonly ICacheManager cacheManager;

        public Ping(IPingRepository pingRepository, ICacheManager cacheManager)
        {
            this.pingRepository = pingRepository;
            this.cacheManager = cacheManager;
        }

        public PingResultViewModel VerifyServiceStatus()
        {
            var result = new PingResultViewModel();
            try
            {
                pingRepository.PingDatabase();
                result.DatabaseStatus = ServiceStatusEnum.Ok.GetDescription();
            }
            catch (Exception)
            {
                result.DatabaseStatus = ServiceStatusEnum.Shutdown.GetDescription();
            }

            try
            {
                cacheManager.GetCache(string.Empty);
                result.CacheServiceStatus = ServiceStatusEnum.Ok.GetDescription();
            }
            catch (Exception)
            {
                result.CacheServiceStatus = ServiceStatusEnum.Shutdown.GetDescription();
            }
            return result;
        }
    }
}
