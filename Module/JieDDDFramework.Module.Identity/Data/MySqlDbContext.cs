using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Module.Identity.Data
{
    public class MySqlDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetIdLengthLimit(modelBuilder);
            base.OnModelCreating(modelBuilder);
            
        }

        protected virtual void SetIdLengthLimit(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var mutableProperty in entityType.FindPrimaryKey().Properties)
                {
                    try
                    {
                        builder.Entity(entityType.ClrType).Property<string>(mutableProperty.Name).HasMaxLength(64);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            
        }
    }
}
