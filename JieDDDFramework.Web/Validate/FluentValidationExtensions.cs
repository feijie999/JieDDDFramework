using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using JieDDDFramework.Web.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Web.Validate
{
    public static class FluentValidationExtensions
    {
        public static IMvcBuilder AddCustomFluentValidation(this IMvcBuilder builder,
            Action<FluentValidationMvcConfiguration> configurationExpression = null)
        {
            builder.Services.AddTransient<IValidator<PagedCriteria>, PagedCriteriaValidator>();
            var executingAssembly = Assembly.GetExecutingAssembly();
            if (configurationExpression == null)
            {
                configurationExpression = x => x.RegisterValidatorsFromAssembly(executingAssembly);
            }

            builder.AddFluentValidation(configurationExpression);
            return builder;
        }
    }
}
