using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Aggregates.BuyerAggregate;

namespace Order.Domain.EntityConfigurations
{
    public class PaymentMethodEntityTypeConfiguration : DDDEntityTypeConfiguration<PaymentMethod>
    {
        public override void Map(EntityTypeBuilder<PaymentMethod> configuration)
        {
            configuration.ToTable("paymentmethods");
            configuration.HasKey(x => x.Id);
            configuration.Property<string>("BuyerId")
                .IsRequired();
            configuration.Property(x => x.FreeCode).HasMaxLength(32);
            configuration.HasOne(x => x.PaymentType)
                .WithMany();
        }
    }
}
