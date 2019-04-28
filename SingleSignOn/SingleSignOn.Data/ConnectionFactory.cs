using MongoDB.Driver;
using SingleSignOn.Data.Interfaces;
using SingleSignOn.Utils;

namespace SingleSignOn.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IMongoDatabase GetDatabase()
        {
            return new MongoClient(AppSettings.MongoDbConnectionString).GetDatabase(AppSettings.MongoDbDatabaseName);
        }
    }
}
