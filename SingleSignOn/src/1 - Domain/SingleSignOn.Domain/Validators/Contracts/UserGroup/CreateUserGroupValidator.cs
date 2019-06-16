using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.ViewModels.UserGroup;
using SingleSignOn.Utils.ExtensionMethods;

namespace SingleSignOn.Domain.Validators.Contracts.UserGroup
{
    public class CreateUserGroupValidator : AbstractValidator<UserGroupViewModel>
    {
        private readonly IUserGroupRepository userGroupRepository;
        private readonly IUserRepository userRepository;

        public CreateUserGroupValidator(IUserGroupRepository userGroupRepository, IUserRepository userRepository)
        {
            this.userGroupRepository = userGroupRepository;
            this.userRepository = userRepository;

            RuleFor(prop => prop.GroupName)
            .NotNull().WithMessage("User group name cannot be null.")
            .NotEmpty().WithMessage("User group name cannot be empty.")
            .Must(prop => !UserGroupExists(prop)).WithMessage("User group with this name, already exists.");

            RuleFor(prop => prop.Users)
            .Must(prop => AllUsersExists(prop)).WithMessage("One or more users provided does not exists.");

            RuleFor(prop => prop.Permissions)
            .Must(prop => AllPermissionsHaveValue(prop)).WithMessage("Permission could not have empty values.");
        }

        private bool AllPermissionsHaveValue(List<string> permissions)
        {
            return !permissions.Any(x => x.IsNullOrEmpty());
        }

        private bool UserGroupExists(string userGroupName)
        {
            return userGroupRepository.GetUserGroupByName(userGroupName).Count > 0;
        }

        private bool AllUsersExists(List<string> usernames)
        {
            return userRepository.GetUsersByUsername(usernames).Count >= usernames.Count;
        }
    }
}