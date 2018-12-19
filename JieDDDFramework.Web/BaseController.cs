using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace JieDDDFramework.Web
{
    public class BaseController : Controller
    {
        [NonAction]
        protected virtual IActionResult Success(string message = "")
        {
            var result = new ApiResult
            {
                Message = message
            };
            result.Succeed();
            return Ok(result);
        }

        [NonAction]
        protected virtual IActionResult Fail(string message = "",int code = -1)
        {
            var result = new ApiResult
            {
                Message = message,
                Code = code
            };
            result.Fail();
            return Ok(result);
        }

        [NonAction]
        protected virtual IActionResult Success(object data, string message = "")
        {
            var result = new ApiResult<object>
            {
                Value = data,
                Message = message
            };
            result.Succeed();
            return Ok(result);
        }

        [NonAction]
        protected virtual IActionResult Error(string message,int code =-1)
        {
            var result = new ApiResult
            {
                Message = message,
                Code = -1
            };
            result.Succeed();
            return Ok(result);
        }
    }
}
