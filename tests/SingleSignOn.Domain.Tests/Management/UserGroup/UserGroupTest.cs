using System.Collections.Generic;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Interfaces.Management.UserGroup;
using SingleSignOn.Domain.ViewModels.UserGroup;
using SingleSignOn.Tests.Shared;
using DomainUserGroup = SingleSignOn.Domain.Management.UserGroup.UserGroup;
using UserGroupDocument = SingleSignOn.Data.Documents.UserGroup;

namespace SingleSignOn.Domain.Tests.Management.UserGroup
{
    public class UserGroupTest : BaseTest
    {
        DomainUserGroup targetClass;
        Mock<IUserGroupRepository> repository;
        Mock<IUserGroupValidator> validator;

        protected override void SetupTest()
        {
            validator = new Mock<IUserGroupValidator>();
            repository = new Mock<IUserGroupRepository>();
            targetClass = new DomainUserGroup(validator.Object, repository.Object);
        }

        protected override void TearDownTest()
        {
            validator = null;
            repository = null;
            targetClass = null;
        }

        [Test]
        public void Will_Create_A_New_UserGroup_Because_It_Passed_In_Validation()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = "MyUserGroupName",
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "THIRD_PERMISSION_RULE" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" }
            };

            targetClass.CreateNewUserGroup(userGroup);

            validator.Verify(_ => _.ValidateToCreateUserGroup(ref userGroup), Times.Exactly(1));
            repository.Verify(_ => _.CreateAsync(It.IsAny<UserGroupDocument>()), Times.Exactly(1));
        }

        [Test]
        public void Will_Not_Create_A_UserGroup_And_Return_Errors_Because_It_Have_Not_Passed_In_Validation()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = "MyUserGroupName",
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "THIRD_PERMISSION_RULE" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" },
                ValidationResult = new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure(string.Empty, "UserGroup with this name already exists.")
                }),
            };

            targetClass.CreateNewUserGroup(userGroup);

            validator.Verify(_ => _.ValidateToCreateUserGroup(ref userGroup), Times.Exactly(1));
            repository.Verify(_ => _.CreateAsync(It.IsAny<UserGroupDocument>()), Times.Never);
        }
    }
}