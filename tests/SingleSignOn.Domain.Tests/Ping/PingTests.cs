using Moq;
using NUnit.Framework;
using SingleSignOn.Caching.Interfaces;
using SingleSignOn.Data.Interfaces.Repositories;
using SingleSignOn.Domain.Enums.Ping;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils.ExtensionMethods;
using System;
using PingClass = SingleSignOn.Domain.Ping.Ping;

namespace SingleSignOn.Domain.Tests.Ping
{
    public class PingTests : BaseTest
    {
        Mock<IPingRepository> pingRepository;
        Mock<ICacheManager> cacheManager;
        PingClass targetClass;

        protected override void SetupTest()
        {
            pingRepository = new Mock<IPingRepository>();
            cacheManager = new Mock<ICacheManager>();
            targetClass = new PingClass(pingRepository.Object, cacheManager.Object);
        }

        protected override void TearDownTest()
        {
            targetClass = null;
            pingRepository = null;
        }

        [Test]
        public void Will_Ping()
        {
            targetClass.VerifyServiceStatus();
            pingRepository.Verify(_ => _.PingDatabase(), Times.Exactly(1));
            cacheManager.Verify(_ => _.GetCache(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Will_Return_Database_Ok_Because_Database_Is_Up()
        {
            var result = targetClass.VerifyServiceStatus();
            pingRepository.Verify(_ => _.PingDatabase(), Times.Exactly(1));
            Assert.AreEqual(ServiceStatusEnum.Ok.GetDescription(), result.DatabaseStatus);
        }

        [Test]
        public void Will_Return_Database_Shutdown_Because_Repository_Throwed_A_Exception()
        {
            pingRepository.Setup(_ => _.PingDatabase()).Throws(new Exception());
            var result = targetClass.VerifyServiceStatus();
            pingRepository.Verify(_ => _.PingDatabase(), Times.Exactly(1));
            Assert.AreEqual(ServiceStatusEnum.Shutdown.GetDescription(), result.DatabaseStatus);
        }

        [Test]
        public void Will_Return_Database_Ok_Because_Caching_Service_Is_Up()
        {
            var result = targetClass.VerifyServiceStatus();
            cacheManager.Verify(_ => _.GetCache(It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(ServiceStatusEnum.Ok.GetDescription(), result.CacheServiceStatus);
        }

        [Test]
        public void Will_Return_Caching_Shutdown_Because_CacheManager_Throwed_A_Exception()
        {
            cacheManager.Setup(_ => _.GetCache(It.IsAny<string>())).Throws(new Exception());
            var result = targetClass.VerifyServiceStatus();
            cacheManager.Verify(_ => _.GetCache(It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(ServiceStatusEnum.Shutdown.GetDescription(), result.CacheServiceStatus);
        }
    }
}
