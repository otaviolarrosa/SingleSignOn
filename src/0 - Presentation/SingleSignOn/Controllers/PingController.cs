using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Domain.Interfaces.Ping;
using System;

namespace SingleSignOn.Controllers
{
    public class PingController : BaseController
    {
        private readonly IPing ping;

        public PingController(IPing ping)
        {
            this.ping = ping;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Ping()
        {
            try
            {
                var result = ping.VerifyServiceStatus();
                return Sucess(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}