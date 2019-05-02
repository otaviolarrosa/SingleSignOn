using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Interfaces.Management.User;
using SingleSignOn.Domain.ViewModels.User;
using UserDocument = SingleSignOn.Data.Documents.User;

namespace SingleSignOn.Domain.Management.User
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository userRepository;
        private readonly IUserValidator userValidator;

        public UserManagement(IUserRepository userRepository, IUserValidator userValidator)
        {
            this.userRepository = userRepository;
            this.userValidator = userValidator;
        }

        public UserViewModel CreateUser(UserViewModel userViewModel)
        {
            userValidator.ValidateCreationOfUser(ref userViewModel);

            if (userViewModel.Invalid)
                return userViewModel;

            var databaseUser = new UserDocument
            {
                Email = userViewModel.Email,
                PasswordHash = userViewModel.PasswordHash,
                UserName = userViewModel.UserName
            };

            userRepository.CreateAsync(databaseUser);
            return userViewModel;
        }
    }
}
