using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.Interfaces.Authentication
{
    public interface IAuthentication
    {
        UserViewModel AuthenticateUser(UserViewModel userViewModel);
    }
}
