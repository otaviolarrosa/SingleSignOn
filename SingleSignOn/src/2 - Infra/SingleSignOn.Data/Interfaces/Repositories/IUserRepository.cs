using SingleSignOn.Data.Documents;
using System.Collections.Generic;

namespace SingleSignOn.Data.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<User> GetUserByUsername(string username);
        List<User> GetUserByEmail(string email);
        List<User> GetUserByUsernameAndPassword(string username, string passwordHashpassword);
    }
}
