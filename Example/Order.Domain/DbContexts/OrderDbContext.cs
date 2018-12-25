using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Exceptions.Utilities;
using JieDDDFramework.Data.EntityFramework.DbContext;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Aggregates.BuyerAggregate;
using Order.Domain.Aggregates.OrderAggregate;

namespace Order.Domain.DbContexts
{
    public class OrderDbContext: DomainDbContext
    {
        public DbSet<Aggregates.OrderAggregate.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> Payments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }

        private readonly IMediator _mediator;
        public OrderDbContext(DbContextOptions options, IMediator mediator, IModelConfigurationProvider modelConfigurationProvider) : base(options, mediator, modelConfigurationProvider)
        {
            Check.NotNull(mediator, nameof(mediator));
            _mediator = mediator;
        }
    }
}
