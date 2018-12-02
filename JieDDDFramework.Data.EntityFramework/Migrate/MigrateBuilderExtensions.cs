using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JieDDDFramework.Data.EntityFramework.Migrate
{
    public static class MigrateBuilderExtensions
    {
        public static MigrateBuilder AddDbSeed<TDbContext>(this MigrateBuilder builder, IDbContextSeed<TDbContext> dbContextSeed) where TDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            builder.Services.AddSingleton(dbContextSeed);
            return builder;
        }
    }
}
