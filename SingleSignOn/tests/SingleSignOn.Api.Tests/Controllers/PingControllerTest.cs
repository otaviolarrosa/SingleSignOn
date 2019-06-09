using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SingleSignOn.Controllers;
using SingleSignOn.Domain.Enums.Ping;
using SingleSignOn.Domain.Interfaces.Ping;
using SingleSignOn.Domain.ViewModels;
using SingleSignOn.Domain.ViewModels.Ping;
using SingleSignOn.Tests.Shared;
using SingleSignOn.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleSignOn.Api.Tests.Controllers
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
            ping.Setup(_ => _.VerifyServiceStatus()).Returns(new PingResultViewModel { DatabaseStatus = ServiceStatusEnum.Ok.GetDescription() });
            var result = targetClass.Ping() as OkObjectResult;
            var objectResult = result.Value as PingResultViewModel;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(ServiceStatusEnum.Ok.GetDescription(), objectResult.DatabaseStatus);
        }

        [Test]
        public void Will_Return_BadRequest_Because_Ping_Is_Successfull_But_Something_Went_Wrong()
        {
            var pingResult = new PingResultViewModel
            {
                DatabaseStatus = ServiceStatusEnum.Shutdown.GetDescription(),
                ValidationResult = new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("someProperty", "someErrorMessage"),
                    new ValidationFailure("someProperty2", "someErrorMessage"),
                    new ValidationFailure("someProperty3", "someErrorMessage"),
                    new ValidationFailure("someProperty4", "someErrorMessage"),
                })
            };

            ping.Setup(_ => _.VerifyServiceStatus()).Returns(pingResult);
            var result = targetClass.Ping() as BadRequestObjectResult;
            var objectResult = result.Value as List<ErrorApiViewModel>;
            Assert.AreEqual(400, result.StatusCode);

            for (int i = 0; i < objectResult.Count; i++)
            {
                Assert.AreEqual(pingResult.ValidationResult.Errors[i].ErrorMessage, objectResult[i].ErrorMessage);
                Assert.AreEqual(pingResult.ValidationResult.Errors[i].PropertyName, objectResult[i].Property);
            }

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
