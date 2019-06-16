using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Interfaces.Management.UserGroup;
using SingleSignOn.Domain.ViewModels.UserGroup;
using UserGroupDocument = SingleSignOn.Data.Documents.UserGroup;

namespace SingleSignOn.Domain.Management.UserGroup
{
    public class UserGroup : IUserGroup
    {
        private readonly IUserGroupValidator validator;
        private readonly IUserGroupRepository repository;

        public UserGroup(IUserGroupValidator validator, IUserGroupRepository repository)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public UserGroupViewModel CreateNewUserGroup(UserGroupViewModel userGroupViewModel)
        {
            validator.ValidateToCreateUserGroup(ref userGroupViewModel);
            if(userGroupViewModel.Valid)
            {
                var userGroupDocument = new UserGroupDocument
                {
                    GroupName = userGroupViewModel.GroupName,
                    Permissions = userGroupViewModel.Permissions,
                    Users = userGroupViewModel.Users
                };
                
                repository.CreateAsync(userGroupDocument);
            }
            return userGroupViewModel;
        }
    }
}