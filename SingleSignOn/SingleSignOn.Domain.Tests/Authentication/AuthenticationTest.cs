using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using SingleSignOn.Domain.Authentication;
using SingleSignOn.Domain.Interfaces.Authentication;
using SingleSignOn.Domain.Interfaces.Jwt;
using SingleSignOn.Domain.ViewModels.User;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils.ExtensionMethods;

namespace SingleSignOn.Domain.Tests.Authentication
{
    public class AuthenticationTest : BaseTest
    {
        Domain.Authentication.Authentication targetClass;
        Mock<IAuthenticationValidator> authenticationValidator;
        Mock<ITokenGenerator> tokenGenerator;

        protected override void SetupTest()
        {
            authenticationValidator = new Mock<IAuthenticationValidator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            targetClass = new Domain.Authentication.Authentication(authenticationValidator.Object, tokenGenerator.Object);
        }

        protected override void TearDownTest()
        {
            authenticationValidator = null;
            targetClass = null;
        }

        [Test]
        public void Will_Generate_Token_For_User_Because_User_Is_Valid()
        {
            string emailLogin = "test_email@provider.com";
            string securePassword = "myMostSecurePassoword".Md5Encypt();

            var userViewModel = new UserViewModel
            {
                Email = emailLogin,
                PasswordHash = securePassword
            };

            tokenGenerator.Setup(m => m.GenerateToken(It.IsAny<string>())).Returns("myJwtTokenSample");
            tokenGenerator.Setup(m => m.GenerateRefreshToken()).Returns("MyRefreshTokenSample");

            var result = targetClass.AuthenticateUser(userViewModel);
            Assert.IsNotEmpty(result.Token);
            Assert.IsNotEmpty(result.RefreshTokens);
            tokenGenerator.Verify(m => m.GenerateRefreshToken(), Times.Exactly(1));
            tokenGenerator.Verify(m => m.GenerateToken(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Will_Not_Generate_Token_For_User_Because_User_Isnt_Valid()
        {
            string emailLogin = "test_email@provider.com";
            string securePassword = "myMostSecurePassoword".Md5Encypt();

            var userViewModel = new UserViewModel
            {
                Email = emailLogin,
                PasswordHash = securePassword,
            };

            userViewModel.ValidationResult.Errors.Add(new ValidationFailure("someProperty", "someErrorMessage"));

            var result = targetClass.AuthenticateUser(userViewModel);

            Assert.IsNull(result.Token);
            Assert.IsEmpty(result.RefreshTokens);
            Assert.IsTrue(userViewModel.Invalid);
            tokenGenerator.Verify(m => m.GenerateRefreshToken(), Times.Never);
            tokenGenerator.Verify(m => m.GenerateToken(It.IsAny<string>()), Times.Never);
        }
    }
}
