using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Domain.Interfaces.Authentication;
using SingleSignOn.Domain.ViewModels.User;
using System;

namespace SingleSignOn.Controllers
{

    public class AuthenticationController : BaseController
    {
        private readonly IAuthentication authentication;

        public AuthenticationController(IAuthentication authentication)
        {
            this.authentication = authentication;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserViewModel userViewModel)
        {
            try
            {
                var result = authentication.AuthenticateUser(userViewModel);
                return Sucess(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
