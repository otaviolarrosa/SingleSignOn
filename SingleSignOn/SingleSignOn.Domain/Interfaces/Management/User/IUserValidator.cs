using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.Interfaces.Management.User
{
    public interface IUserValidator
    {
        void ValidateCreationOfUser(ref UserViewModel userViewModel);
    }
}
