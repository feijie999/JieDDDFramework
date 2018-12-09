using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations.Services
{
    public class DefaultFixModelConfigurationService : IFixModelConfigurationService
    {
        public void FixModel<TDbContext>(ModelBuilder modelBuilder, TDbContext dbContext) where TDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            SetIdLengthLimit(modelBuilder);
        }

        protected virtual void SetIdLengthLimit(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var mutableProperty in entityType.FindPrimaryKey().Properties)
                {
                    try
                    {
                        builder.Entity(entityType.ClrType).Property<string>(mutableProperty.Name).HasMaxLength(64);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }
    }
}
