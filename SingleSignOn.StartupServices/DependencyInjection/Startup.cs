using Microsoft.Extensions.DependencyInjection;
using SingleSignOn.Caching;
using SingleSignOn.Data.Repositories;
using SingleSignOn.Domain.Ping;

namespace SingleSignOn.StartupServices.DependencyInjection
{
    public class Startup
    {
        public void Start(IServiceCollection services)
        {
            services.Scan(_ => _
            .FromAssemblies(
                typeof(Ping).Assembly, 
                typeof(PingRepository).Assembly,
                typeof(CacheManager).Assembly)
            .AddClasses()
            .AsImplementedInterfaces());
        }
    }
}
