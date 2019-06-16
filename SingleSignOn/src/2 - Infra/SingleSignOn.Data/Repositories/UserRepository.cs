using MongoDB.Driver;
using SingleSignOn.Data.Documents;
using SingleSignOn.Data.Interfaces;
using SingleSignOn.Data.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

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

        public List<User> GetUsersByUsername(List<string> usernames)
        {
            var query = from e in base.collection.AsQueryable<User>()
                        select e;
            usernames.ForEach(username => { query.Where(_ => _.UserName == username); });
            return query.ToList();
        }

        public List<User> GetUserByEmail(string email)
        {
            return base.collection.Find(prop => prop.Email == email).ToList();
        }

        public List<User> GetUserByUsername(string username)
        {
            return base.collection.Find(prop => prop.UserName == username).ToList();
        }

        public List<User> GetUserByUsernameAndPassword(string username, string passwordHash)
        {
            return base.collection.Find(prop => prop.UserName == username && prop.PasswordHash == passwordHash).ToList();
        }
    }
}
