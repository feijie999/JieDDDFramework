using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.DbContext;
using JieDDDFramework.Data.EntityFramework.Repositories;
using JieDDDFramework.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Data.EntityFramework
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository<TDbContext>(this IServiceCollection services) where TDbContext : DomainDbContext
        {
            services.AddScoped<DomainDbContext, TDbContext>();
            services.AddTransient(typeof(IRepositoryBase<>), typeof(Repository<>));
            return services;
        }
    }
}
