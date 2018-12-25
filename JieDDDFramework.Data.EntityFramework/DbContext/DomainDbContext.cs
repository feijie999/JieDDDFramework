using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JieDDDFramework.Core.Domain;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Data.EntityFramework.DbContext
{
    public abstract class DomainDbContext :Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork
    {
        protected readonly IMediator Mediator;
        protected readonly IModelConfigurationProvider ModelConfigurationProvider;

        protected DomainDbContext(DbContextOptions options, IMediator mediator, IModelConfigurationProvider modelConfigurationProvider) : base(options)
        {
            Mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            ModelConfigurationProvider = modelConfigurationProvider;
        }

        protected DomainDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            Mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (ModelConfigurationProvider != null)
            {
                ModelConfigurationProvider.GetFixModelConfigurationService().FixModel(modelBuilder, this);
                ModelConfigurationProvider.GetGlobalFilterService().QueryFilter(modelBuilder, this);
                ModelConfigurationProvider.GetApplyConfigurationService().AutoApplyConfiguration(modelBuilder, this);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
