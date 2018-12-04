using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
        private readonly IHostingEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
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

                if (env.IsDevelopment())
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
