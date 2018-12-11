using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                var keys = entityType.FindPrimaryKey();
                if (keys == null)
                {
                    foreach (var property in entityType.GetProperties().Where(x => x.ClrType == typeof(string)))
                    {
                        var propertyBuilder =builder.Entity(entityType.ClrType).Property<string>(property.Name);
                        var length = propertyBuilder.Metadata.GetMaxLength();
                        if (length == null)
                        {
                            propertyBuilder.HasMaxLength(64);
                        }
                    }
                    continue;
                }
                foreach (var mutableProperty in keys.Properties)
                {
                    try
                    {
                        if (mutableProperty.ClrType == typeof(string))
                        {
                            builder.Entity(entityType.ClrType).Property<string>(mutableProperty.Name).HasMaxLength(64);
                        }
                    }
                    catch(Exception e)
                    {
                    }
                }
            }
        }
    }
}
