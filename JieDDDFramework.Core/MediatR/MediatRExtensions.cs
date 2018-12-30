using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.MediatR.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Core.MediatR
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddDefaultMediatRBehaviors(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            return services;
        }
    }
}
