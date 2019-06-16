using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SingleSignOn.Controllers;
using SingleSignOn.Domain.Interfaces.Management.User;
using SingleSignOn.Domain.ViewModels;
using SingleSignOn.Domain.ViewModels.User;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace SingleSignOn.Api.Tests.Controllers
{
    public class UserControllerTest : BaseTest
    {
        Mock<IUserManagement> userManagement;
        UserController targetClass;

        protected override void SetupTest()
        {
            userManagement = new Mock<IUserManagement>();
            targetClass = new UserController(userManagement.Object);
        }

        protected override void TearDownTest()
        {
            targetClass = null;
            userManagement = null;
        }

        [Test]
        public void Will_Return_OK_Because_Domain_Created_A_User_Successfully()
        {
            var userResult = new UserViewModel
            {
                Email = "test@email.com",
                PasswordHash = "someString".Md5Encypt(),
                UserName = "Username Test"
            };
            userManagement.Setup(_ => _.CreateUser(It.IsAny<UserViewModel>())).Returns(userResult);
            var result = targetClass.CreateUser(userResult) as OkObjectResult;
            var objectResult = result.Value as UserViewModel;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(userResult.Email, objectResult.Email);
            Assert.AreEqual(userResult.UserName, objectResult.UserName);
        }

        [Test]
        public void Will_Return_BadRequest_With_Errors_Because_Domain_Failed_In_Validation()
        {
            var userCreateResult = new UserViewModel
            {
                ValidationResult = new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("someProperty", "someErrorMessage"),
                    new ValidationFailure("someProperty2", "someErrorMessage"),
                    new ValidationFailure("someProperty3", "someErrorMessage"),
                    new ValidationFailure("someProperty4", "someErrorMessage"),
                })
            };

            userManagement.Setup(_ => _.CreateUser(It.IsAny<UserViewModel>())).Returns(userCreateResult);
            var result = targetClass.CreateUser(new UserViewModel()) as BadRequestObjectResult;
            var objectResult = result.Value as List<ErrorApiViewModel>;
            Assert.AreEqual(400, result.StatusCode);

            for (int i = 0; i < objectResult.Count; i++)
            {
                Assert.AreEqual(userCreateResult.ValidationResult.Errors[i].ErrorMessage, objectResult[i].ErrorMessage);
                Assert.AreEqual(userCreateResult.ValidationResult.Errors[i].PropertyName, objectResult[i].Property);
            }
        }

        [Test]
        public void Will_Return_Internal_Server_Error_Because_Domain_Throwed_A_Non_Expected_Exceptio()
        {
            userManagement.Setup(_ => _.CreateUser(It.IsAny<UserViewModel>())).Throws(new Exception());
            var result = targetClass.CreateUser(new UserViewModel()) as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
