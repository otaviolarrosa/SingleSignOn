using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.Interfaces.Authentication
{
    public interface IAuthenticationValidator
    {
        void ValidateToAuthenticateUser(ref UserViewModel userViewModel); 
    }
}
