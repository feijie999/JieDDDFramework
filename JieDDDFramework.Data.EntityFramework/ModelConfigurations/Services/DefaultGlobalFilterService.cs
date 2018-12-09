using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations.Services
{
    public class DefaultGlobalFilterService: IGlobalFilterService
    {
        private readonly ModelConfigurationOption _option;
        public DefaultGlobalFilterService(ModelConfigurationOption option)
        {
            _option = option;
        }
        public void QueryFilter<TDbContext>(ModelBuilder modelBuilder, TDbContext dbContext) where TDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var mutableProperty in mutableEntityType.GetProperties().Where(x => _option.QueryFilterFields.Contains(x.Name)||x.ClrType == typeof(bool)))
                {
                    var parameter = Expression.Parameter(mutableEntityType.ClrType, "x");
                    var body = Expression.Equal(Expression.Call(
                        typeof(EF), nameof(EF.Property), new[] {mutableProperty.ClrType}, parameter,
                        Expression.Constant(mutableProperty.Name)), Expression.Constant(false));
                    modelBuilder.Entity(mutableEntityType.ClrType).HasQueryFilter(Expression.Lambda(body,parameter));
                }
            }
        }
    }
}
