using Microsoft.Extensions.Configuration;
using SingleSignOn.Utils;
using SingleSignOn.Utils.ExtensionMethods;

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
            AppSettings.SecretKey = configuration.GetSection("Auth:SecretKey").Value;
            AppSettings.ExpireTokenInMinutes = configuration.GetSection("Auth:ExpireTokenInMinutes").Value.ToInt32();
        }
    }
}
