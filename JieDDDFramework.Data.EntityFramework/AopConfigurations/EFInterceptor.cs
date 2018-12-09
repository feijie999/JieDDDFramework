using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Data.EntityFramework.AopConfigurations
{
    public class EFInterceptor : AbstractInterceptor
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var modelConfigurationProvider = context.ServiceProvider.GetService<IModelConfigurationProvider>();
            if (modelConfigurationProvider == null)
            {
                throw new ArgumentNullException(nameof(modelConfigurationProvider));
            }

            var modelBuilder = (ModelBuilder) context.Parameters[0];
            var dbContext = (Microsoft.EntityFrameworkCore.DbContext) context.Proxy;
            modelConfigurationProvider.GetFixModelConfigurationService().FixModel(modelBuilder, dbContext);
            modelConfigurationProvider.GetGlobalFilterService().QueryFilter(modelBuilder, dbContext);
            modelConfigurationProvider.GetApplyConfigurationService().AutoApplyConfiguration(modelBuilder,dbContext);
            await context.Invoke(next);

        }
    }
}
