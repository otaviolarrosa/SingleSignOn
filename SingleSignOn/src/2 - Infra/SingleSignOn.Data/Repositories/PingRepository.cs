using SingleSignOn.Data.Interfaces;
using SingleSignOn.Data.Interfaces.Repositories;

namespace SingleSignOn.Data.Repositories
{
    public class PingRepository : IPingRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public PingRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void PingDatabase()
        {
            connectionFactory.GetDatabase().ListCollectionNames();
        }
    }
}