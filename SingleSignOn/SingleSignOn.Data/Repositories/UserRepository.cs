using MongoDB.Driver;
using SingleSignOn.Data.Documents;
using SingleSignOn.Data.Interfaces;
using SingleSignOn.Data.Interfaces.Repositories;
using System.Collections.Generic;

namespace SingleSignOn.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory) : base()
        {
            this.connectionFactory = connectionFactory;
            this.connectionFactory.SetCollectionName("Users");
            collection = connectionFactory.GetCollection<User>();
        }

        public List<User> GetUserByEmail(string email)
        {
            return base.collection.Find(prop => prop.Email == email).ToList();
        }

        public List<User> GetUserByUsername(string username)
        {
            return base.collection.Find(prop => prop.UserName == username).ToList();
        }
    }
}
