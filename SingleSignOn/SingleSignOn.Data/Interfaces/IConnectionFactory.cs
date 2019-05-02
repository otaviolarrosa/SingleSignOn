using MongoDB.Driver;
using SingleSignOn.Data.Documents;

namespace SingleSignOn.Data.Interfaces
{
    public interface IConnectionFactory
    {
        IMongoDatabase GetDatabase();
        IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : BaseDocument;
        void SetCollectionName(string value);
        string GetCollectionName();
    }
}
