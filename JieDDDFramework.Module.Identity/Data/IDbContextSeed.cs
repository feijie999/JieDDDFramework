using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JieDDDFramework.Module.Identity.Data
{
    public interface IDbContextSeed<TDbContext> where TDbContext : DbContext
    {
        Task SeedAsync<TSetting>(TDbContext context, IHostingEnvironment evn,
        ILogger<IDbContextSeed<TDbContext>> logger, IOptions<TSetting> settings, int? retry = 0) where TSetting : class, new();
    }
}
