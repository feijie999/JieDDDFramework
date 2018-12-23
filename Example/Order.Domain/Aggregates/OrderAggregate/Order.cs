using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;

namespace Order.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity<string>, IAggregateRoot
    {
    }
}
