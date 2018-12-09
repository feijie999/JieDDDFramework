using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JieDDDFramework.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Data.EntityFramework.DbContext
{
    public abstract class DomainDbContext :Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork
    {
        protected readonly IMediator Mediator;

        private DomainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DomainDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            Mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
