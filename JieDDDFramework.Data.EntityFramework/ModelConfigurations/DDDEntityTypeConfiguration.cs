using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations
{
    public abstract class DDDEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class,IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> buyerConfiguration)
        {
            buyerConfiguration.Ignore(b => b.DomainEvents);
            Map(buyerConfiguration);
        }

        public abstract void Map(EntityTypeBuilder<TEntity> buyerConfiguration);
    }
}
