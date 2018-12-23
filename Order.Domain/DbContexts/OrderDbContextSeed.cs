using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JieDDDFramework.Data.EntityFramework.Migrate;

namespace Order.Domain.DbContexts
{
    public class OrderDbContextSeed : IDbContextSeed<OrderDbContext>
    {
        public Task SeedAsync(OrderDbContext context)
        {
            return Task.CompletedTask;
        }
    }
}
