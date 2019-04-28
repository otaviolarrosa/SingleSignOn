using MongoDB.Driver;

namespace SingleSignOn.Data.Interfaces
{
    public interface IConnectionFactory
    {
        IMongoDatabase GetDatabase();
    }
}
