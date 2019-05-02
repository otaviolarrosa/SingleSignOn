using FluentValidation;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.ViewModels.User;
using SingleSignOn.Utils.ExtensionMethods;

namespace SingleSignOn.Domain.Validators.Contracts.User
{
    internal class CreateUserValidator : AbstractValidator<UserViewModel>
    {
        private readonly IUserRepository userRepository;

        public CreateUserValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(ent => ent)
                .Must(_ => !UserWithEmailExists(_.Email)).WithMessage("A user with this email is already registered.")
                .Must(_ => !UserNameExists(_.UserName)).WithMessage("Username already exists.");

            RuleFor(_ => _.Email)
                .EmailAddress();

            RuleFor(_ => _.UserName)
                .MinimumLength(6);

            RuleFor(_ => _.PasswordHash)
                .NotEmpty()
                .Must(__ => __.IsValidMd5()).WithMessage("Password must be a encrypted value.");
        }

        public bool UserWithEmailExists(string email)
        {
            return userRepository.GetUserByEmail(email).Count > 0;
        }

        public bool UserNameExists(string username)
        {
            return userRepository.GetUserByUsername(username).Count > 0;
        }
    }
}
