using System.Threading.Tasks;

namespace JieDDDFramework.Data.EntityFramework.Migrate
{
    public interface IDbContextSeed<in TDbContext> where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        Task SeedAsync<TSetting>(TDbContext context,int? retry = 0) where TSetting : class, new();
    }
}
