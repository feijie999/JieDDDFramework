using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Aggregates.BuyerAggregate;

namespace Order.Domain.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : DDDEntityTypeConfiguration<Aggregates.OrderAggregate.Order>
    {
        public override void Map(EntityTypeBuilder<Aggregates.OrderAggregate.Order> orderConfiguration)
        {
            orderConfiguration.ToTable("orders");

            orderConfiguration.HasKey(o => o.Id);
            
            orderConfiguration.OwnsOne(o => o.Address);
            orderConfiguration.Property(x => x.CreatedTime).IsRequired();
            orderConfiguration.HasOne(x => x.Buyer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.BuyerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            orderConfiguration.HasOne(x => x.OrderStatus)
                .WithMany()
                .HasForeignKey(x => x.OrderStatusId);

            orderConfiguration.HasOne(x => x.PaymentMethod)
                .WithMany()
                .HasForeignKey(x => x.PaymentMethodId);

            orderConfiguration.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            //var navigation = orderConfiguration.Metadata.FindNavigation(nameof(Aggregates.OrderAggregate.Order.OrderItems));
            
            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            //orderConfiguration.HasOne<PaymentMethod>()
            //    .WithMany()
            //    .HasForeignKey("PaymentMethodId")
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);

            //orderConfiguration.HasOne<Buyer>()
            //    .WithMany()
            //    .IsRequired(false)
            //    .HasForeignKey("BuyerId");

            //orderConfiguration.HasOne(o => o.OrderStatus)
            //    .WithMany()
            //    .HasForeignKey("OrderStatusId");
        }
    }
}
