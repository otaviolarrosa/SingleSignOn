using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Management.UserGroup;
using SingleSignOn.Domain.ViewModels.UserGroup;
using SingleSignOn.Tests.Shared;
using UserGroupDocument = SingleSignOn.Data.Documents.UserGroup;
using UserDocument = SingleSignOn.Data.Documents.User;

namespace SingleSignOn.Domain.Tests.Management.UserGroup
{
    public class UserGroupValidatorTest : BaseTest
    {
        UserGroupValidator targetClass;
        Mock<IUserGroupRepository> userGroupRepository;
        Mock<IUserRepository> userRepository;

        protected override void SetupTest()
        {
            userGroupRepository = new Mock<IUserGroupRepository>();
            userRepository = new Mock<IUserRepository>();
            targetClass = new UserGroupValidator(userGroupRepository.Object, userRepository.Object);
        }

        protected override void TearDownTest()
        {
            userGroupRepository = null;
            userRepository = null;
            targetClass = null;
        }

        [Test]
        public void Will_Return_Ok_Because_Validation_Passed()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = "MyUserGroupName",
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "THIRD_PERMISSION_RULE" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" }
            };

            var firstUser = new UserDocument
            {
                UserName = "firstUserName"
            };

            var secondUser = new UserDocument
            {
                UserName = "secondUserName"
            };

            var thirdUser = new UserDocument
            {
                UserName = "thirdUserName"
            };

            userGroupRepository.Setup(_ => _.GetUserGroupByName(It.IsAny<string>())).Returns(new List<UserGroupDocument> { });
            userRepository.Setup(_ => _.GetUsersByUsername(It.IsAny<List<string>>())).Returns(new List<UserDocument> { firstUser, secondUser, thirdUser });

            targetClass.ValidateToCreateUserGroup(ref userGroup);
            Assert.IsTrue(userGroup.Valid);
            Assert.IsFalse(userGroup.Invalid);
            Assert.IsEmpty(userGroup.ValidationResult.Errors);
        }

        [Test]
        public void Will_Return_Error_Because_UserGroup_Name_Cannot_Be_Empty()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = string.Empty,
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "THIRD_PERMISSION_RULE" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" }
            };

            userGroupRepository.Setup(_ => _.GetUserGroupByName(It.IsAny<string>())).Returns(new List<UserGroupDocument> { });
            userRepository.Setup(_ => _.GetUsersByUsername(It.IsAny<List<string>>())).Returns(new List<UserDocument> { });

            targetClass.ValidateToCreateUserGroup(ref userGroup);
            Assert.IsFalse(userGroup.Valid);
            Assert.IsTrue(userGroup.Invalid);
            Assert.IsNotEmpty(userGroup.ValidationResult.Errors);
            Assert.AreEqual("User group name cannot be empty.", userGroup.ValidationResult.Errors.FirstOrDefault().ErrorMessage);
            Assert.AreEqual("GroupName", userGroup.ValidationResult.Errors.FirstOrDefault().PropertyName);
        }

        [Test]
        public void Will_Return_Error_Because_UserGroup_Name_Cannot_Be_Null()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = null,
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "THIRD_PERMISSION_RULE" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" }
            };

            userGroupRepository.Setup(_ => _.GetUserGroupByName(It.IsAny<string>())).Returns(new List<UserGroupDocument> { });
            userRepository.Setup(_ => _.GetUsersByUsername(It.IsAny<List<string>>())).Returns(new List<UserDocument> { });

            targetClass.ValidateToCreateUserGroup(ref userGroup);
            Assert.IsFalse(userGroup.Valid);
            Assert.IsTrue(userGroup.Invalid);
            Assert.IsNotEmpty(userGroup.ValidationResult.Errors);
            Assert.AreEqual("User group name cannot be null.", userGroup.ValidationResult.Errors.FirstOrDefault().ErrorMessage);
            Assert.AreEqual("GroupName", userGroup.ValidationResult.Errors.FirstOrDefault().PropertyName);
        }


        [Test]
        public void Will_Return_Error_Because_UserGroup_With_Name_Already_Exists()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = "MyUserGroupName",
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "THIRD_PERMISSION_RULE" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" }
            };

            var userGroupDocument = new UserGroupDocument
            {
                GroupName = userGroup.GroupName,
                Permissions = userGroup.Permissions,
                Users = userGroup.Users
            };

            userGroupRepository.Setup(_ => _.GetUserGroupByName(It.IsAny<string>())).Returns(new List<UserGroupDocument> { userGroupDocument });
            userRepository.Setup(_ => _.GetUsersByUsername(It.IsAny<List<string>>())).Returns(new List<UserDocument> { });

            targetClass.ValidateToCreateUserGroup(ref userGroup);
            Assert.IsFalse(userGroup.Valid);
            Assert.IsTrue(userGroup.Invalid);
            Assert.IsNotEmpty(userGroup.ValidationResult.Errors);
            Assert.AreEqual("User group with this name, already exists.", userGroup.ValidationResult.Errors.FirstOrDefault().ErrorMessage);
            Assert.AreEqual("GroupName", userGroup.ValidationResult.Errors.FirstOrDefault().PropertyName);
        }

        [Test]
        public void Will_Return_Error_Because_Some_Of_Users_Provided_Doesnt_Exists_In_Database()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = "MyUserGroupName",
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "THIRD_PERMISSION_RULE" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" }
            };

            var firstUser = new UserDocument
            {
                UserName = "firstUserName"
            };

            var secondUser = new UserDocument
            {
                UserName = "secondUserName"
            };

            var thirdUser = new UserDocument
            {
                UserName = "thirdUserName"
            };

            var userGroupDocument = new UserGroupDocument
            {
                GroupName = userGroup.GroupName,
                Permissions = userGroup.Permissions,
            };

            userGroupRepository.Setup(_ => _.GetUserGroupByName(It.IsAny<string>())).Returns(new List<UserGroupDocument> { });
            userRepository.Setup(_ => _.GetUsersByUsername(It.IsAny<List<string>>())).Returns(new List<UserDocument>
            {
                firstUser, secondUser
            });

            targetClass.ValidateToCreateUserGroup(ref userGroup);
            Assert.IsFalse(userGroup.Valid);
            Assert.IsTrue(userGroup.Invalid);
            Assert.IsNotEmpty(userGroup.ValidationResult.Errors);
            Assert.AreEqual("One or more users provided does not exists.", userGroup.ValidationResult.Errors.FirstOrDefault().ErrorMessage);
            Assert.AreEqual("Users", userGroup.ValidationResult.Errors.FirstOrDefault().PropertyName);
        }

        [Test]
        public void Will_Return_Error_Because_Some_Of_Permissions_Provided_Is_Empty()
        {
            var userGroup = new UserGroupViewModel
            {
                GroupName = "MyUserGroupName",
                Permissions = new List<string> { "FIRST_PERMISSION_RULE", "SECOND_PERMISSION_RULE", "" },
                Users = new List<string> { "firstUserName", "secondUserName", "thirdUserName" }
            };

            var firstUser = new UserDocument
            {
                UserName = "firstUserName"
            };

            var secondUser = new UserDocument
            {
                UserName = "secondUserName"
            };

            var thirdUser = new UserDocument
            {
                UserName = "thirdUserName"
            };

            var userGroupDocument = new UserGroupDocument
            {
                GroupName = userGroup.GroupName,
                Permissions = userGroup.Permissions,
            };

            userGroupRepository.Setup(_ => _.GetUserGroupByName(It.IsAny<string>())).Returns(new List<UserGroupDocument> { });
            userRepository.Setup(_ => _.GetUsersByUsername(It.IsAny<List<string>>())).Returns(new List<UserDocument>
            {
                firstUser, secondUser, thirdUser
            });

            targetClass.ValidateToCreateUserGroup(ref userGroup);
            Assert.IsFalse(userGroup.Valid);
            Assert.IsTrue(userGroup.Invalid);
            Assert.IsNotEmpty(userGroup.ValidationResult.Errors);
            Assert.AreEqual("Permission could not have empty values.", userGroup.ValidationResult.Errors.FirstOrDefault().ErrorMessage);
            Assert.AreEqual("Permissions", userGroup.ValidationResult.Errors.FirstOrDefault().PropertyName);
        }
    }
}