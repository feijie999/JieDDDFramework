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
        public virtual void Configure(EntityTypeBuilder<TEntity> configuration)
        {
            configuration.Ignore(b => b.DomainEvents);
            Map(configuration);
        }

        public abstract void Map(EntityTypeBuilder<TEntity> configuration);
    }
}
