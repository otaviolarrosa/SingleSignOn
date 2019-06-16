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
        private readonly IUserRepository userRepository;

        public UserGroupValidator(IUserGroupRepository userGroupRepository, IUserRepository userRepository)
        {
            this.userGroupRepository = userGroupRepository;
            this.userRepository = userRepository;
        }

        public void ValidateToCreateUserGroup(ref UserGroupViewModel userGroupViewModel)
        {
            base.Validate(ref userGroupViewModel, new CreateUserGroupValidator(userGroupRepository, userRepository));
        }
    }
}