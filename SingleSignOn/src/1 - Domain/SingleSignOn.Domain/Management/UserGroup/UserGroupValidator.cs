using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Interfaces.Management.UserGroup;
using SingleSignOn.Domain.Validators;
using SingleSignOn.Domain.Validators.Contracts.UserGroup;
using SingleSignOn.Domain.ViewModels.UserGroup;

namespace SingleSignOn.Domain.Management.UserGroup
{
    public class UserGroupValidator : BaseValidator<UserGroupViewModel>, IUserGroupValidator
    {
        private readonly IUserGroupRepository userGroupRepository;
        public UserGroupValidator(IUserGroupRepository userGroupRepository)
        {
            this.userGroupRepository = userGroupRepository;
        }

        public void ValidateToCreateUserGroup(ref UserGroupViewModel userGroupViewModel)
        {
            base.Validate(ref userGroupViewModel, new CreateUserGroupValidator(userGroupRepository));
        }
    }
}