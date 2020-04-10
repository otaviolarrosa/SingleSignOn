using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SingleSignOn.Api.Controllers;
using SingleSignOn.Domain.Interfaces.Authentication;
using SingleSignOn.Domain.ViewModels.User;
using SingleSignOn.Tests.Shared;
using System.Collections.Generic;
using SingleSignOn.Utils.ExtensionMethods;
using FluentValidation.Results;
using SingleSignOn.Domain.ViewModels;

namespace SingleSignOn.Api.Tests.Controllers
{
    public class AuthenticationControllerTest : BaseTest
    {
        Mock<IAuthentication> authentication;
        AuthenticationController targetClass;

        protected override void SetupTest()
        {
            authentication = new Mock<IAuthentication>();
            targetClass = new AuthenticationController(authentication.Object);
        }

        protected override void TearDownTest()
        {
            authentication = null;
            targetClass = null;
        }

        [Test]
        public void Will_Return_Ok_Because_Authentication_Is_Successfull()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                Token = "someFakeToken",
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };

            authentication.Setup(_ => _.AuthenticateUser(It.IsAny<UserViewModel>())).Returns(user);
            var result = targetClass.Login(user) as OkObjectResult;
            var objectResult = result.Value as UserViewModel;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(user.Token, objectResult.Token);
        }

        [Test]
        public void Will_Return_BadRequest_Because_Ping_Is_Successfull_But_Something_Went_Wrong()
        {
            var user = new UserViewModel
            {
                ValidationResult = new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("someProperty", "someErrorMessage"),
                    new ValidationFailure("someProperty2", "someErrorMessage"),
                    new ValidationFailure("someProperty3", "someErrorMessage"),
                    new ValidationFailure("someProperty4", "someErrorMessage"),
                })
            };

            authentication.Setup(_ => _.AuthenticateUser(It.IsAny<UserViewModel>())).Returns(user);
            var result = targetClass.Login(user) as BadRequestObjectResult;
            var objectResult = result.Value as List<ErrorApiViewModel>;
            Assert.AreEqual(400, result.StatusCode);

            for (int i = 0; i < objectResult.Count; i++)
            {
                Assert.AreEqual(user.ValidationResult.Errors[i].ErrorMessage, objectResult[i].ErrorMessage);
                Assert.AreEqual(user.ValidationResult.Errors[i].PropertyName, objectResult[i].Property);
            }

        }

        [Test]
        public void Will_Return_InternalServerError_Because_Ping_Is_Unsuccessfull()
        {
            authentication.Setup(_ => _.AuthenticateUser(It.IsAny<UserViewModel>())).Throws(new System.Exception());
            var result = targetClass.Login(new UserViewModel()) as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

    }
}
