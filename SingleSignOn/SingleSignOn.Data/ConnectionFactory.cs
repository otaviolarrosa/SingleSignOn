using MongoDB.Driver;
using SingleSignOn.Data.Documents;
using SingleSignOn.Data.Interfaces;
using SingleSignOn.Utils;

namespace SingleSignOn.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IMongoDatabase database;

        private string collectionName;

        public IMongoDatabase GetDatabase()
        {
            database = new MongoClient(AppSettings.MongoDbConnectionString).GetDatabase(AppSettings.MongoDbDatabaseName);
            return database;
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : BaseDocument
        {
            if (database.IsNull())
            {
                database = new MongoClient(AppSettings.MongoDbConnectionString).GetDatabase(AppSettings.MongoDbDatabaseName);
            }

            return database.GetCollection<TDocument>(this.collectionName);
        }

        public string GetCollectionName() => collectionName;

        public void SetCollectionName(string value) => collectionName = value;
    }
}
