using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Domain.ViewModels;
using SingleSignOn.Utils.ExtensionMethods;

namespace SingleSignOn.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Sucess(BaseViewModel result)
        {
            if (result.Invalid)
            {
                var errors = new List<ErrorApiViewModel>();
                result.ValidationResult.Errors.ToList().ForEach(item =>
                {
                    errors.Add(new ErrorApiViewModel(item.PropertyName, item.ErrorMessage));
                });

                return BadRequest(errors);
            }
            return Ok(result);
        }

        protected IActionResult InternalServerError(Exception ex)
        {
            return StatusCode(HttpStatusCode.InternalServerError.ToInt(), ex);
        }
    }
}
