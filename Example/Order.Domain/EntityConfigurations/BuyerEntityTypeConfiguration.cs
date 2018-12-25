using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Aggregates.BuyerAggregate;

namespace Order.Domain.EntityConfigurations
{
    public class BuyerEntityTypeConfiguration
        : DDDEntityTypeConfiguration<Buyer>
    {
        public override void Map(EntityTypeBuilder<Buyer> buyerConfiguration)
        {
            buyerConfiguration.ToTable("buyers");

            buyerConfiguration.HasKey(b => b.Id);

            buyerConfiguration.Property(b => b.Name).IsRequired().HasMaxLength(32);

            buyerConfiguration.HasMany(b => b.PaymentMethods)
                .WithOne()
                .HasForeignKey("BuyerId")
                .OnDelete(DeleteBehavior.Cascade);

            //var navigation = buyerConfiguration.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));
            buyerConfiguration.Property(x => x.PaymentMethods);
            //navigation.SetPropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        }
    }
}
