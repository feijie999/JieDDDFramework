using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.Configuration;
using AspectCore.Injector;
using Autofac;

namespace JieDDDFramework.Data.EntityFramework.AopConfigurations
{
    public static class ServiceContainerExtensions
    {
        public static InterceptorCollection ConfigureEFInterceptors(this InterceptorCollection interceptorCollection)
        {
            interceptorCollection.AddTyped<EFInterceptor>(Predicates.ForMethod("*DbContext", "OnModelCreating"));

            return interceptorCollection;
        }
    }
}
