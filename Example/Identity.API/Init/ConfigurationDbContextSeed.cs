using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using JieDDDFramework.Data.EntityFramework.Migrate;
using JieDDDFramework.Module.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Identity.API.Init
{
    public class ConfigurationDbContextSeed : IDbContextSeed<ConfigurationDbContext>
    {
        private readonly IConfiguration _configuration;

        public ConfigurationDbContextSeed(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SeedAsync(ConfigurationDbContext context)
        {
            var clientUrls = new Dictionary<string, string>();

            clientUrls.Add("Mvc", _configuration.GetValue<string>("MvcClient"));
            clientUrls.Add("OrderApi", _configuration.GetValue<string>("OrderClient"));
            clientUrls.Add("IdentityApi", _configuration.GetValue<string>("IdentityClient"));
            var secret = _configuration.GetSection("JwtSettings").GetValue<string>("SecretKey");
#if DEBUG
            var clients = context.Clients.ToList();
            context.RemoveRange(clients);
            await context.SaveChangesAsync();
#endif
            if (!context.Clients.Any())
            {
                foreach (var client in IdentityServerSeed.GetClients(clientUrls, secret))
                {
                    context.Clients.Add(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }
            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerSeed.GetResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }
            if (!context.ApiResources.Any())
            {
                foreach (var api in IdentityServerSeed.GetApis())
                {
                    context.ApiResources.Add(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
