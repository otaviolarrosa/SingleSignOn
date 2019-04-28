using Microsoft.Extensions.Configuration;
using SingleSignOn.Utils;

namespace SingleSignOn.StartupServices.GlobalSettings
{
    public class Startup
    {
        public void Start(IConfiguration configuration)
        {
            AppSettings.MongoDbConnectionString = configuration.GetSection("MongoDatabase:ConnectionString").Value;
            AppSettings.MongoDbDatabaseName = configuration.GetSection("MongoDatabase:DatabaseName").Value;
            AppSettings.RedisHost = configuration.GetSection("RedisCaching:RedisHost").Value;
            AppSettings.RedisInstanceName = configuration.GetSection("RedisCaching:RedisInstanceName").Value;
        }
    }
}
