using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Interfaces.Authentication;
using SingleSignOn.Domain.Validators;
using SingleSignOn.Domain.Validators.Contracts.Authentication;
using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.Authentication
{
    public class AuthenticationValidator : BaseValidator<UserViewModel>, IAuthenticationValidator
    {
        private readonly IUserRepository userRepository;

        public AuthenticationValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void ValidateToAuthenticateUser(ref UserViewModel userViewModel)
        {
            Validate(ref userViewModel, new AuthenticateUserValidator(userRepository));
        }
    }
}
