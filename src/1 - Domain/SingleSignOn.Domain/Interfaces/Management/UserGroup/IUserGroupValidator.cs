using SingleSignOn.Domain.ViewModels.UserGroup;

namespace SingleSignOn.Domain.Interfaces.Management.UserGroup
{
    public interface IUserGroupValidator
    {
         void ValidateToCreateUserGroup(ref UserGroupViewModel userGroupViewModel);
    }
}