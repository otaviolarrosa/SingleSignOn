using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.Interfaces.Management.User
{
    public interface IUserManagement
    {
        UserViewModel CreateUser(UserViewModel userViewModel);
    }
}
