using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JieDDDFramework.Core.Domain;
using JieDDDFramework.Data.EntityFramework.Migrate;
using Microsoft.EntityFrameworkCore.Internal;
using Order.Domain.Aggregates.BuyerAggregate;
using Order.Domain.Aggregates.OrderAggregate;

namespace Order.Domain.DbContexts
{
    public class OrderDbContextSeed : IDbContextSeed<OrderDbContext>
    {
        public async Task SeedAsync(OrderDbContext context)
        {
            if (!context.OrderStatus.Any())
            {
                await context.OrderStatus.AddRangeAsync(OrderStatus.List());
            }
            if (!context.PaymentTypes.Any())
            {
                await context.PaymentTypes.AddRangeAsync(Enumeration.GetAll<PaymentType>());
            }
            await context.SaveChangesAsync();
        }
    }
}
