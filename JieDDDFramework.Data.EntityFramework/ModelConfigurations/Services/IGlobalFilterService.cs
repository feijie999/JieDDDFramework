using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations.Services
{
    public interface IGlobalFilterService
    {
        void QueryFilter<TDbContext>(ModelBuilder modelBuilder, TDbContext dbContext) where TDbContext : Microsoft.EntityFrameworkCore.DbContext;
    }
}
