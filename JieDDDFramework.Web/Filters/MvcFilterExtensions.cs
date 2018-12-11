using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Web.Filters
{
    public static class MvcFilterExtensions
    {
        public static IMvcBuilder AddCustomFilter(this IMvcBuilder builder)
        {
            builder.Services.AddScoped<ValidateModelStateFilter>();
            builder.Services.AddScoped<HttpGlobalExceptionFilter>();
            builder.AddMvcOptions(x =>
            {
                x.Filters.AddService<ValidateModelStateFilter>();
                x.Filters.AddService<HttpGlobalExceptionFilter>();
            });
            return builder;
        }
    }
}
