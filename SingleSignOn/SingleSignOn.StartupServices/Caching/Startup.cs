using Microsoft.Extensions.DependencyInjection;
using SingleSignOn.Utils;

namespace SingleSignOn.StartupServices.Caching
{
    public class Startup
    {
        public void Start(IServiceCollection services)
        {
            services.AddDistributedRedisCache(_ =>
            {
                _.Configuration = AppSettings.RedisHost;
                _.InstanceName = AppSettings.RedisInstanceName;
            });
        }
    }
}
