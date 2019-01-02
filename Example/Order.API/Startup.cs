using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using JieDDDFramework.Core.Configures;
using JieDDDFramework.Core.MediatR;
using JieDDDFramework.Data.EntityFramework;
using JieDDDFramework.Data.EntityFramework.AopConfigurations;
using JieDDDFramework.Data.EntityFramework.Migrate;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using JieDDDFramework.Data.EntityFramework.Repositories;
using JieDDDFramework.Data.Repository;
using JieDDDFramework.Module.Identity;
using JieDDDFramework.Module.Identity.Data;
using JieDDDFramework.Module.Identity.Models;
using JieDDDFramework.Web.Filters;
using JieDDDFramework.Web.Validate;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Order.API.Models;
using Order.Domain.Application.Commands;
using StackExchange.Redis;
using Order.Domain.DbContexts;
using Swashbuckle.AspNetCore.Swagger;

namespace Order.API
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
                .AddCustomFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<OrderAddedViewModelValidator>();
                    x.RegisterValidatorsFromAssemblyContaining<CreateOrderCommand.CreateOrderCommandValidator>();
                })
                .AddControllersAsServices();
            var settings = services.ConfigureOption(Configuration, () => new AppSettings());
            if (settings.IsClusterEnv)
            {
                services.AddDataProtection(opts => { opts.ApplicationDiscriminator = "Order.API"; })
                    .PersistKeysToRedis(ConnectionMultiplexer.Connect(settings.RedisConnectionString));
            }

            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<OrderDbContext>(options =>
                {
                    options.UseMySql(settings.ConnectionString,
                        sqlOptions => { sqlOptions.MigrationsAssembly(assemblyName); });
                })
                .AddRepository<OrderDbContext>();
            services.AddMigrateService()
                .AddDbSeed(new OrderDbContextSeed());
            services.AddEFModelConfiguration();
            services.AddMediatR(typeof(CreateOrderCommandHandler))
                .AddDefaultMediatRBehaviors();

            services.AddJwtAuthentication(Configuration.GetSection("JwtSettings"));
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
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseSwagger()
                .UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderServerAPI");
                    x.OAuthClientId("orderswaggerui");
                });
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services,
            IConfiguration configuration)
        {
            var setting = services.ConfigureOption(configuration.GetSection("JwtSettings"), () => new JwtSettings());
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "订单API",
                    Version = "v1",
                    TermsOfService = "None",
                    Description = ""
                });
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme()
                {
                    Flow = "implicit",
                    AuthorizationUrl = setting.Issuer + "/connect/authorize",
                    TokenUrl = setting.Issuer + "/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"order", "Identity API"}
                    }
                });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"{Bearer}{空格}{token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(security);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, typeof(Startup).Namespace + ".xml"));
            });

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var setting = services.ConfigureOption(configuration, () => new JwtSettings());
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
                {
                    o.Authority = setting.Issuer;
                    o.Audience = setting.Audience;
                    o.RequireHttpsMetadata = false;
                });
            return services;
        }
    }
}
