using System.Collections.Generic;
using MongoDB.Driver;
using SingleSignOn.Data.Documents;
using SingleSignOn.Data.Interfaces;
using SingleSignOn.Data.Interfaces.Repositories;

namespace SingleSignOn.Data.Repositories
{
    public class UserGroupRepository : BaseRepository<UserGroup>, IUserGroupRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public UserGroupRepository(IConnectionFactory connectionFactory): base()
        {
            this.connectionFactory = connectionFactory;
            this.connectionFactory.SetCollectionName("UserGroups");
            collection = connectionFactory.GetCollection<UserGroup>();
            this.connectionFactory = connectionFactory;
        }

        public List<UserGroup> GetUserGroupByName(string userGroupName)
        {
            return base.collection.Find(prop => prop.GroupName == userGroupName).ToList();
        }
    }
}