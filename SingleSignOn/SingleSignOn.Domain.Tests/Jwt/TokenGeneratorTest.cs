using NUnit.Framework;
using SingleSignOn.Domain.Jwt;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils;
using SingleSignOn.Utils.ExtensionMethods;

namespace SingleSignOn.Domain.Tests.Jwt
{
    public class TokenGeneratorTest : BaseTest
    {
        TokenGenerator targetClass;

        protected override void SetupTest()
        {
            targetClass = new TokenGenerator();
        }

        protected override void TearDownTest()
        {
            targetClass = null;
        }


        [Test]
        public void Will_Generate_A_Jwt_Token_With_Username_In_Claims()
        {
            AppSettings.SecretKey = "someSecretKey".Md5Encypt().ToBase64();
            AppSettings.ExpireTokenInMinutes = 5;
            var username = "someUsername";

            var result = targetClass.GenerateToken(username);

            Assert.IsTrue(result.IsValidJwtToken());
        }

        [Test]
        public void Will_Generate_A_New_Base64_From_A_RandomNumber_As_RefreshToken()
        {
            var result = targetClass.GenerateRefreshToken();
            var decodedBase64 = result.FromBase64ToByteArray();
            Assert.NotZero(decodedBase64.ToInt());
        }

        [Test]
        public void Will_Get_ClaimPrincipal_From_A_Generated_Token()
        {
            AppSettings.SecretKey = "someSecretKey".Md5Encypt().ToBase64();
            AppSettings.ExpireTokenInMinutes = 5;
            var username = "someUsername";
            var token = targetClass.GenerateToken(username);
            var result = targetClass.GetPrincipalFromExpiredToken(token);
            Assert.AreEqual(username, result.Identity.Name);

        }

    }
}
