using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;

namespace Order.Domain.Aggregates.BuyerAggregate
{
    public class Buyer : Entity<string>, IAggregateRoot
    {
        public string Name { get; }
        public Pay
    }
}
