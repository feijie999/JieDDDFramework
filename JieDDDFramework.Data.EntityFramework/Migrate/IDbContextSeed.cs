using System.Threading.Tasks;

namespace JieDDDFramework.Data.EntityFramework.Migrate
{
    public interface IDbContextSeed<in TDbContext> where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        Task SeedAsync(TDbContext context);
    }
}
