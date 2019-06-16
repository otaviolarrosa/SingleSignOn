using SingleSignOn.Domain.ViewModels.UserGroup;

namespace SingleSignOn.Domain.Interfaces.Management.UserGroup
{
    public interface IUserGroup
    {
         UserGroupViewModel CreateNewUserGroup(UserGroupViewModel userGroupViewModel);
    }
}
