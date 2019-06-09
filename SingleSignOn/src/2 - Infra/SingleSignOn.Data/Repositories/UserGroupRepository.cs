using System.Collections.Generic;
using MongoDB.Driver;
using SingleSignOn.Data.Documents;
using SingleSignOn.Data.Interfaces.Repositories;

namespace SingleSignOn.Data.Repositories
{
    public class UserGroupRepository : BaseRepository<UserGroup>, IUserGroupRepository
    {
        public List<UserGroup> GetUserGroupByName(string userGroupName)
        {
            return base.collection.Find(prop => prop.GroupName == userGroupName).ToList();
        }
    }
}