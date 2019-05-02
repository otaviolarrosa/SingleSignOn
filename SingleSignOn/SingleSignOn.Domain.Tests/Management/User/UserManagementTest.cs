using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using UserDocument = SingleSignOn.Data.Documents.User;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Interfaces.Management.User;
using SingleSignOn.Domain.Management;
using SingleSignOn.Domain.ViewModels.User;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils.ExtensionMethods;
using System.Collections.Generic;
using System.Threading.Tasks;
using SingleSignOn.Domain.Management.User;

namespace SingleSignOn.Domain.Tests.Management.User
{
    public class UserManagementTest : BaseTest
    {
        UserManagement targetClass;
        Mock<IUserRepository> userRepository;
        Mock<IUserValidator> userValidator;

        protected override void SetupTest()
        {
            userRepository = new Mock<IUserRepository>();
            userValidator = new Mock<IUserValidator>();
            targetClass = new UserManagement(userRepository.Object, userValidator.Object);
        }

        protected override void TearDownTest()
        {
            userRepository = null;
            userValidator = null;
            targetClass = null;
        }

        [Test]
        public void Will_Create_A_New_User_Because_It_Passed_In_Validation()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };
            userValidator.Setup(_ => _.ValidateCreationOfUser(ref user));
            userRepository.Setup(_ => _.CreateAsync(It.IsAny<UserDocument>())).Returns(Task.FromResult("someId".Md5Encypt()));

            targetClass.CreateUser(user);

            userValidator.Verify(_ => _.ValidateCreationOfUser(ref user), Times.Exactly(1));
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<UserDocument>()), Times.Exactly(1));
        }

        [Test]
        public void Will_Not_Create_A_User_And_Return_Errors_Because_It_Have_Not_Passed_In_Validation()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
                ValidationResult = new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure(string.Empty, "User already exists.")
                }),
            };

            targetClass.CreateUser(user);

            userValidator.Verify(_ => _.ValidateCreationOfUser(ref user), Times.Exactly(1));
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<UserDocument>()), Times.Never);
        }
    }
}
