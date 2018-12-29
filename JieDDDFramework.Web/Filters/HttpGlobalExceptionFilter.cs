using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FluentValidation;
using JieDDDFramework.Core.Exceptions;
using JieDDDFramework.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace JieDDDFramework.Web.Filters
{
    public partial class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this._env = env;
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception is KnownException knownException)
            {
                var result = new ApiResult()
                {
                    Success = false,
                    Code = knownException.ErrorCode,
                    Message = knownException.Message
                };
                if (knownException.InnerException is ValidationException validationException)
                {
                    var errorInfo = validationException.Errors.FirstOrDefault();
                    if (errorInfo!=null)
                    {
                        if (int.TryParse(errorInfo.ErrorCode,out var errorCode))
                        {
                            result.Code = errorCode;
                        }
                        result.Message = errorInfo.ErrorMessage;
                    }
                }
                context.Result = new BadRequestObjectResult(result);
            }
            else
            {
                var result = new ApiResult()
                {
                    Success = false,
                    Code = -500,
                    Message = "An error occurred. Try it again."
                };

                if (_env.IsDevelopment())
                {
                    result.Message = context.Exception.GetAllMessages();
                }

                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int) HttpStatusCode.InternalServerError
                };
            }
            context.ExceptionHandled = true;
        }
    }
}
