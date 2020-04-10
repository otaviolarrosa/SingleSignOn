using System.Collections.Generic;
using SingleSignOn.Data.Documents;

namespace SingleSignOn.Data.Interfaces.Repositories
{
    public interface IUserGroupRepository :  IBaseRepository<UserGroup>
    {
        List<UserGroup> GetUserGroupByName(string userGroupName);
    }
}