using FluentValidation;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.ViewModels.UserGroup;

namespace SingleSignOn.Domain.Validators.Contracts.UserGroup
{
    public class CreateUserGroupValidator : AbstractValidator<UserGroupViewModel>
    {
        private readonly IUserGroupRepository userGroupRepository;
        public CreateUserGroupValidator(IUserGroupRepository userGroupRepository)
        {
            this.userGroupRepository = userGroupRepository;
            RuleFor(prop => prop.GroupName)
            .NotNull().WithMessage("User group name cannot be null.")
            .NotEmpty().WithMessage("User group name cannot be empty.")
            .Must(prop => !UserGroupExists(prop)).WithMessage("User group with this name, already exists.");;
        }

        private bool UserGroupExists(string userGroupName)
        {
            return userGroupRepository.GetUserGroupByName(userGroupName).Count > 0;
        }
    }
}