using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Domain.Interfaces.Management.User;
using SingleSignOn.Domain.ViewModels.User;
using System;

namespace SingleSignOn.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserManagement userManagement;

        public UserController(IUserManagement userManagement)
        {
            this.userManagement = userManagement;
        }

        [HttpPost]
        public IActionResult CreateUser(UserViewModel userViewModel)
        {
            try
            {
                var result = userManagement.CreateUser(userViewModel);
                return base.Sucess(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
