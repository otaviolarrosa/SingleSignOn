using Moq;
using NUnit.Framework;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Management.User;
using SingleSignOn.Domain.ViewModels.User;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using UserDocument = SingleSignOn.Data.Documents.User;

namespace SingleSignOn.Domain.Tests.Management.User
{
    public class UserValidatorTest : BaseTest
    {
        UserValidator targetClass;
        Mock<IUserRepository> userRepository;

        protected override void SetupTest()
        {
            userRepository = new Mock<IUserRepository>();
            targetClass = new UserValidator(userRepository.Object);
        }

        protected override void TearDownTest()
        {
            userRepository = null;
            targetClass = null;
        }

        [Test]
        public void Will_Return_Error_Because_Username_Already_Exists_In_Database()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };
            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { new UserDocument { } });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });

            targetClass.ValidateCreationOfUser(ref user);

            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual(string.Empty, user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual("Username already exists.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Return_Error_Because_Email_Is_Not_Valid()
        {
            var user = new UserViewModel
            {
                Email = "email.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };

            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });

            targetClass.ValidateCreationOfUser(ref user);


            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual("Email", user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual(@"'Email' is not a valid email address.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Return_Error_Because_Password_Is_Not_A_Valid_Md5_Hash()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword",
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };

            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });
            targetClass.ValidateCreationOfUser(ref user);

            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual("PasswordHash", user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual("Password must be a encrypted value.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Return_Error_Because_Email_Is_Already_Registered()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };


            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { new UserDocument { } });
            targetClass.ValidateCreationOfUser(ref user);

            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual(string.Empty, user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual("A user with this email is already registered.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Return_Error_Because_Username_Is_Empty()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = string.Empty,
            };

            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });
            targetClass.ValidateCreationOfUser(ref user);


            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual("UserName", user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual(@"The length of 'User Name' must be at least 6 characters. You entered 0 characters.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Return_Error_Because_Password_Hash_Is_Empty()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = string.Empty,
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };

            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });
            targetClass.ValidateCreationOfUser(ref user);

            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual("PasswordHash", user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual(@"'Password Hash' must not be empty.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Return_Error_Because_Email_Is_Empty()
        {
            var user = new UserViewModel
            {
                Email = string.Empty,
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };

            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });
            targetClass.ValidateCreationOfUser(ref user);


            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual("Email", user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual(@"'Email' is not a valid email address.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Return_Error_Because_Username_Is_Too_Short()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "user1",
            };

            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });
            targetClass.ValidateCreationOfUser(ref user);


            Assert.IsFalse(user.Valid);
            Assert.IsTrue(user.Invalid);
            Assert.IsNotEmpty(user.ValidationResult.Errors);
            Assert.AreEqual("UserName", user.ValidationResult.Errors.First().PropertyName);
            Assert.AreEqual("The length of 'User Name' must be at least 6 characters. You entered 5 characters.", user.ValidationResult.Errors.First().ErrorMessage);
        }

        [Test]
        public void Will_Not_Return_Error_Because_User_Is_Valid_For_Insertion()
        {
            var user = new UserViewModel
            {
                Email = "email@test.com",
                PasswordHash = "somePassword".Md5Encypt(),
                RefreshTokens = new List<string>(),
                UserName = "someUsername",
            };
            userRepository.Setup(_ => _.GetUserByUsername(It.IsAny<string>())).Returns(new List<UserDocument> { });
            userRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new List<UserDocument> { });
            targetClass.ValidateCreationOfUser(ref user);
            Assert.IsTrue(user.Valid);
            Assert.IsFalse(user.Invalid);
            Assert.IsEmpty(user.ValidationResult.Errors);
        }
    }
}
