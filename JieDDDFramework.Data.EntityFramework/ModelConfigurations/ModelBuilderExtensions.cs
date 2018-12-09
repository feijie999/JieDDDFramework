using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AspectCore.Extensions.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations
{
    public static class ModelBuilderExtensions
    {
        public static void AutoApplyConfiguration<TDbContext>(this ModelBuilder modelBuilder, TDbContext dbContext,
            string @namespace = null) where TDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            var typesToRegister = dbContext.GetType().Assembly.GetTypes()
                .Where(x => x.BaseType != null && x.BaseType.IsGenericType &&
                            x.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                .Where(x => string.IsNullOrEmpty(@namespace) || x.Namespace == @namespace);
            foreach (var type in typesToRegister)
            {
                var constructorInfo = type.GetTypeInfo().GetConstructor(new Type[0]);
                var reflector = constructorInfo.GetReflector();
                dynamic configurationInstance = reflector.Invoke();
                //dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
