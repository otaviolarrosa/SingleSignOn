using FluentValidation;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.Validators.Contracts.Authentication
{
    public class AuthenticateUserValidator : BaseValidator<UserViewModel>
    {
        private readonly IUserRepository userRepository;

        public AuthenticateUserValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            RuleFor(ent => ent)
                .Must(UserWithUsernameAndPasswordExists).WithMessage("Username or password does not match.");
        }

        private bool UserWithUsernameAndPasswordExists(UserViewModel userViewModel)
        {
            return userRepository.GetUserByUsernameAndPassword(userViewModel.UserName, userViewModel.PasswordHash).Count > 0;
        }
    }
}
