using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.Autofac;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Identity.API.Init;
using JieDDDFramework.Core.Configures;
using JieDDDFramework.Data.EntityFramework.AopConfigurations;
using JieDDDFramework.Data.EntityFramework.Migrate;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
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
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddCustomFilter()
                .AddCustomFluentValidation()
                .AddControllersAsServices();
            var settings = services.ConfigureOption(Configuration, () => new AppSettings());
            if (settings.IsClusterEnv)
            {
                services.AddDataProtection(opts => { opts.ApplicationDiscriminator = "Identity.API"; })
                    .PersistKeysToRedis(ConnectionMultiplexer.Connect(settings.RedisConnectionString));
            }

            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            void OptionActions(DbContextOptionsBuilder option)
            {
                option.UseMySql(settings.ConnectionString,
                    sqlOptions => { sqlOptions.MigrationsAssembly(assemblyName); });
            }

            services.AddDbContext<IdentityUserDbContext>(OptionActions);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityUserDbContext>()
                .AddDefaultTokenProviders();

            services.AddMigrateService()
                .AddDbSeed(new ConfigurationDbContextSeed(Configuration))
                .AddDbSeed(new IdentityUserDbContextSeed());
            services.AddEFModelConfiguration();

            var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(Convert.FromBase64String(settings.RsaPrivateKey));
            services.AddIdentityServer()
                .AddSigningCredential(new RsaSecurityKey(rsa))
                .AddDefaultIdentityServerConfig<ApplicationUser>(OptionActions);
            services.AddCustomSwagger(Configuration);
            var builder = new ContainerBuilder();
            builder.RegisterDynamicProxy(configure => configure.Interceptors.ConfigureEFInterceptors());
            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseForwardedHeaders();
            app.UseMvcWithDefaultRoute();
            app.UseSwagger()
                .UseSwaggerUI(x=>x.SwaggerEndpoint("/swagger/v1/swagger.json","IdentityServerAPI"));
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "授权认证API",
                    Version = "v1",
                    TermsOfService = "None",
                    Description = "提供oauth2的授权服务"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,typeof(Startup).Namespace+".xml"));
            });

            return services;
        }
    }
}
