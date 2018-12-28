using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Aggregates.OrderAggregate;

namespace Order.Domain.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : DDDEntityTypeConfiguration<OrderItem>
    {
        public override void Map(EntityTypeBuilder<OrderItem> configuration)
        {
            configuration.ToTable("orderItems");
            configuration.Property(x => x.ProductId).IsRequired().HasMaxLength(32);
            configuration.Property(x => x.ProductName).IsRequired().HasMaxLength(32);

        }
    }
}
