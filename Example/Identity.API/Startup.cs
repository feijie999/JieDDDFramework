using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using JieDDDFramework.Core.Configures;
using JieDDDFramework.Module.Identity;
using JieDDDFramework.Module.Identity.Data;
using JieDDDFramework.Module.Identity.Models;
using JieDDDFramework.Web.Filters;
using JieDDDFramework.Web.Validate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.EntityFrameworkCore.Infraestructure;
using MySql.Data.MySqlClient;
using StackExchange.Redis;

namespace Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddCustomFilter()
                .AddCustomFluentValidation();
            var settings = services.ConfigureOption(Configuration, () => new AppSettings());
            if (settings.IsClusterEnv)
            {
                services.AddDataProtection(opts => { opts.ApplicationDiscriminator = "Identity.API"; })
                    .PersistKeysToRedis(ConnectionMultiplexer.Connect(settings.RedisConnectionString));
            }

            var dbConnection = new MySqlConnection(settings.ConnectionStrings.DefaultConnection);

            void OptionActions(DbContextOptionsBuilder option)
            {
                option.UseMySQL(dbConnection, sqlOptions => { sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name); });
            }

            services.AddDbContext<IdentityUserDbContext>(OptionActions);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityUserDbContext>()
                .AddDefaultTokenProviders();

            var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(Convert.FromBase64String(settings.RsaPrivateKey));
            services.AddIdentityServer()
                .AddSigningCredential(new RsaSecurityKey(rsa))
                .AddAspNetIdentity<ApplicationUser>()
                .AddDefaultIdentityServerConfig<ApplicationUser>(OptionActions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
