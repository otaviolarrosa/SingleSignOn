using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SingleSignOn.Controllers;
using SingleSignOn.Domain.Enums.Ping;
using SingleSignOn.Domain.Interfaces.Ping;
using SingleSignOn.Domain.Tests;
using SingleSignOn.Domain.ViewModels.Ping;
using SingleSignOn.Utils.ExtensionMethods;
using System;

namespace SingleSignOn.Api.Tests
{
    public class PingControllerTest : BaseTest
    {
        Mock<IPing> ping;
        PingController targetClass;

        protected override void SetupTest()
        {
            ping = new Mock<IPing>();
            targetClass = new PingController(ping.Object);
        }

        protected override void TearDownTest()
        {
            ping = null;
            targetClass = null;
        }

        [Test]
        public void Will_Return_Ok_Because_Ping_Is_Successfull()
        {
            ping.Setup(_ => _.VerifyServiceStatus()).Returns(new PingResultViewModel { DatabaseStatus = ServiceStatusEnum.Ok.GetDescription()});
            var result = targetClass.Ping() as OkObjectResult;
            var objectResult = result.Value as PingResultViewModel;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(ServiceStatusEnum.Ok.GetDescription(), objectResult.DatabaseStatus);
        }

        [Test]
        public void Will_Return_BadRequest_Because_Ping_Is_Successfull_But_Something_Went_Wrong()
        {
            ping.Setup(_ => _.VerifyServiceStatus()).Returns(new PingResultViewModel { DatabaseStatus = ServiceStatusEnum.Shutdown.GetDescription()});
            var result = targetClass.Ping() as OkObjectResult;
            var objectResult = result.Value as PingResultViewModel;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(ServiceStatusEnum.Shutdown.GetDescription(), objectResult.DatabaseStatus);
        }

        [Test]
        public void Will_Return_InternalServerError_Because_Ping_Is_Unsuccessfull()
        {
            ping.Setup(_ => _.VerifyServiceStatus()).Throws(new Exception());
            var result = targetClass.Ping() as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
