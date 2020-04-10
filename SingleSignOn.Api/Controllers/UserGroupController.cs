using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Domain.Interfaces.Management.UserGroup;
using SingleSignOn.Domain.ViewModels.UserGroup;

namespace SingleSignOn.Api.Controllers
{
    public class UserGroupController : BaseController
    {
        private readonly IUserGroup userGroup;
        public UserGroupController(IUserGroup userGroup)
        {
            this.userGroup = userGroup;
        }

        [HttpPost]
        public IActionResult CreateUserGroup(UserGroupViewModel userGroupViewModel)
        {
            try
            {
                var result = userGroup.CreateNewUserGroup(userGroupViewModel);
                return base.Sucess(result);
            }
            catch (Exception ex)
            {
                return base.InternalServerError(ex);
            }
        }
    }
}