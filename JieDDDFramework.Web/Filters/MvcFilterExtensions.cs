using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using JieDDDFramework.Web.Models;
using JieDDDFramework.Web.ModelValidate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Web.Filters
{
    public static class MvcFilterExtensions
    {
        public static IMvcBuilder AddCustomFilter(this IMvcBuilder builder)
        {
            //builder.Services.AddScoped<ValidateModelStateFilter>();
            builder.Services.AddScoped<HttpGlobalExceptionFilter>();
            builder.AddMvcOptions(x =>
            {
                //x.Filters.AddService<ValidateModelStateFilter>();
                x.Filters.AddService<HttpGlobalExceptionFilter>();
            });
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    if (context.HttpContext.Items.TryGetValue(ValidatorAttribute.VALIDATOR_ITEM, out var obj))
                    {
                        if (obj is IList<ValidationFailure> validationFailures && validationFailures.Any())
                        {
                            return new BadRequestObjectResult(new ModelErrorResult(validationFailures.First()));
                        }
                    }
                    return null;
                };
            });
            return builder;
        }
    }
}
