using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Order.Domain.DbContexts
{
    public class OrderDbContext: DomainDbContext
    {
        public OrderDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
        {
        }
    }
}
