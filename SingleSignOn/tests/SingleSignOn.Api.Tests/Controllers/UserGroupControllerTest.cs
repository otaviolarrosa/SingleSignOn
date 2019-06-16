using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SingleSignOn.Controllers;
using SingleSignOn.Domain.Interfaces.Management.UserGroup;
using SingleSignOn.Domain.ViewModels;
using SingleSignOn.Domain.ViewModels.Permissions;
using SingleSignOn.Domain.ViewModels.UserGroup;
using SingleSignOn.Tests.Shared;

namespace SingleSignOn.Api.Tests.Controllers
{
    public class UserGroupControllerTest : BaseTest
    {
        UserGroupController targetClass;
        Mock<IUserGroup> userGroup;


        protected override void SetupTest()
        {
            userGroup = new Mock<IUserGroup>();
            targetClass = new UserGroupController(userGroup.Object);
        }

        protected override void TearDownTest()
        {
            userGroup = null;
            targetClass = null;
        }

        [Test]
        public void Will_Return_OK_Because_Domain_Created_A_UserGroup_Successfully()
        {
            var userGroupResult = new UserGroupViewModel
            {
                GroupName = "My Test Group Name",
                Permissions = new List<string>()
            };

            userGroup.Setup(_ => _.CreateNewUserGroup(It.IsAny<UserGroupViewModel>())).Returns(userGroupResult);
            var result = targetClass.CreateUserGroup(userGroupResult) as OkObjectResult;
            var objectResult = result.Value as UserGroupViewModel;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(userGroupResult.GroupName, objectResult.GroupName);
        }

        [Test]
        public void Will_Return_BadRequest_With_Errors_Because_Domain_Failed_In_Validation()
        {
            var userGroupResult = new UserGroupViewModel
            {
                ValidationResult = new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("someProperty", "someErrorMessage"),
                    new ValidationFailure("someProperty2", "someErrorMessage"),
                    new ValidationFailure("someProperty3", "someErrorMessage"),
                    new ValidationFailure("someProperty4", "someErrorMessage"),
                })
            };

            userGroup.Setup(_ => _.CreateNewUserGroup(It.IsAny<UserGroupViewModel>())).Returns(userGroupResult);
            var result = targetClass.CreateUserGroup(new UserGroupViewModel()) as BadRequestObjectResult;
            var objectResult = result.Value as List<ErrorApiViewModel>;
            Assert.AreEqual(400, result.StatusCode);

            for (int i = 0; i < objectResult.Count; i++)
            {
                Assert.AreEqual(userGroupResult.ValidationResult.Errors[i].ErrorMessage, objectResult[i].ErrorMessage);
                Assert.AreEqual(userGroupResult.ValidationResult.Errors[i].PropertyName, objectResult[i].Property);
            }
        }

        [Test]
        public void Will_Return_Internal_Server_Error_Because_Domain_Throwed_A_Non_Expected_Exceptio()
        {
            userGroup.Setup(_ => _.CreateNewUserGroup(It.IsAny<UserGroupViewModel>())).Throws(new Exception());
            var result = targetClass.CreateUserGroup(new UserGroupViewModel()) as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}