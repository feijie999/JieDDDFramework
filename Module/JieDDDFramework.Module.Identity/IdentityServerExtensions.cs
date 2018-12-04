using System;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.Services;
using JieDDDFramework.Module.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Module.Identity
{
    public static class IdentityServerExtensions
    {
       public static IIdentityServerBuilder AddDefaultIdentityServerConfig<TApplicationUser>(this IIdentityServerBuilder builder,Action<DbContextOptionsBuilder> dbContextOptionsBuilder = null) where TApplicationUser : ApplicationUser
       {
           builder.AddAspNetIdentity<TApplicationUser>()
               .AddConfigurationStore(options => options.ConfigureDbContext = dbContextOptionsBuilder)
               .AddOperationalStore(options => options.ConfigureDbContext = dbContextOptionsBuilder)
               .Services.AddTransient<IProfileService, Services.ProfileService<TApplicationUser>>(); ;
            return builder;
        }
    }
}
