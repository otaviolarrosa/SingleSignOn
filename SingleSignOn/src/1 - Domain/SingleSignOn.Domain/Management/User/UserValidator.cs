using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Interfaces.Management.User;
using SingleSignOn.Domain.Validators;
using SingleSignOn.Domain.Validators.Contracts.User;
using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.Management.User
{
    public class UserValidator : BaseValidator<UserViewModel>, IUserValidator
    {
        private readonly IUserRepository userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void ValidateCreationOfUser(ref UserViewModel userViewModel)
        {
            Validate(ref userViewModel, new CreateUserValidator(userRepository));
        }
    }
}
