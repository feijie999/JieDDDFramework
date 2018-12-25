using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;
using JieDDDFramework.Core.Exceptions.Utilities;

namespace Order.Domain.Aggregates.BuyerAggregate
{
    public class Buyer : Entity<string>, IAggregateRoot
    {
        public string Name { get; }
        public virtual ICollection<PaymentMethod> PaymentMethods { get; }

        protected Buyer()
        {
            PaymentMethods = new List<PaymentMethod>();
        }

        public Buyer(string id, string name) : this()
        {
            base.Id = Check.NotEmpty(id, nameof(id));
            Name = Check.NotEmpty(name, nameof(name));
        }
    }
}
