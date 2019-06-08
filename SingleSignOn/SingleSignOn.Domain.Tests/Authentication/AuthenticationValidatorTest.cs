using Moq;
using NUnit.Framework;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Authentication;
using SingleSignOn.Domain.ViewModels.User;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UserDocument = SingleSignOn.Data.Documents.User;

namespace SingleSignOn.Domain.Tests.Authentication
{
    public class AuthenticationValidatorTest : BaseTest
    {
        AuthenticationValidator targetClass;
        Mock<IUserRepository> userRepository;

        protected override void SetupTest()
        {
            userRepository = new Mock<IUserRepository>();
            targetClass = new AuthenticationValidator(userRepository.Object);
        }

        protected override void TearDownTest()
        {
            userRepository = null;
            targetClass = null;
        }

        [Test]
        public void Will_Return_A_Valid_Object_Because_Object_Is_Ok()
        {
            string emailLogin = "test_email@provider.com";
            string securePassword = "myMostSecurePassoword".Md5Encypt();

            var userViewModel = new UserViewModel
            {
                Email = emailLogin,
                PasswordHash = securePassword
            };

            var result = new List<UserDocument>
            {
                new UserDocument { Email = emailLogin, PasswordHash = securePassword }
            };

            userRepository.Setup(x => x.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(result);
            targetClass.ValidateToAuthenticateUser(ref userViewModel);
            Assert.IsFalse(userViewModel.Invalid);
            Assert.IsTrue(userViewModel.Valid);
            Assert.IsEmpty(userViewModel.ValidationResult.Errors);
        }

        [Test]
        public void Will_Return_Error_Because_Username_Or_Userpassword_Not_Exists()
        {
            string emailLogin = "test_email@provider.com";
            string securePassword = "myMostSecurePassoword".Md5Encypt();

            var userViewModel = new UserViewModel
            {
                Email = emailLogin,
                PasswordHash = securePassword
            };

            var result = new List<UserDocument> { };

            userRepository.Setup(x => x.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(result);
            targetClass.ValidateToAuthenticateUser(ref userViewModel);
            Assert.IsTrue(userViewModel.Invalid);
            Assert.IsFalse(userViewModel.Valid);
            Assert.IsNotEmpty(userViewModel.ValidationResult.Errors);
            Assert.AreEqual(userViewModel.ValidationResult.Errors.FirstOrDefault().ErrorMessage, "Username or password does not match.");
        }
    }
}
