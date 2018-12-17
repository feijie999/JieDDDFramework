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
            //由于EF自动生成的迁移类会寻找DbContext派生类所在的命名空间，当启用AspectCore的AOP功能后DbContext将会被代理到AspectCore中
            //暂时注释掉
            //interceptorCollection.AddTyped<EFInterceptor>(Predicates.ForMethod("*DbContext", "OnModelCreating"));

            return interceptorCollection;
        }
    }
}
