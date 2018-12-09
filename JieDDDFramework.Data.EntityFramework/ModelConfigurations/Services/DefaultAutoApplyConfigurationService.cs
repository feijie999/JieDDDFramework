using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations.Services
{
    public class DefaultAutoApplyConfigurationService : IAutoApplyConfigurationService
    {
        private readonly ModelConfigurationOption _option;
        public DefaultAutoApplyConfigurationService(ModelConfigurationOption option)
        {
            _option = option;
        }

        public void AutoApplyConfiguration<TDbContext>(ModelBuilder modelBuilder, TDbContext dbContext)
            where TDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            var @namespace = _option.DbModelConfigurationNamespaceDictionary.Where(x => x.Key == dbContext.GetType())
                .Select(x => x.Value).FirstOrDefault();
            modelBuilder.AutoApplyConfiguration(dbContext, @namespace);
        }
    }
}
