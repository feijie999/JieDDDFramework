using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;
using JieDDDFramework.Core.Exceptions.Utilities;

namespace Order.Domain.Aggregates.BuyerAggregate
{
    public class Buyer : Entity<string>, IAggregateRoot
    {
        private readonly List<PaymentMethod> _paymentMethods;
        public virtual IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods;

        private readonly List<OrderAggregate.Order> _orders;

        public virtual IReadOnlyCollection<OrderAggregate.Order> Orders => _orders;

        public string Name { get; private set; }

        public Buyer()
        {
            _orders = new List<OrderAggregate.Order>();
            _paymentMethods = new List<PaymentMethod>();
        }

        public Buyer(string id, string name) : this()
        {
            base.Id = Check.NotEmpty(id, nameof(id));
            Name = Check.NotEmpty(name, nameof(name));
        }

        public void VerifyOrAddPaymentMethod(PaymentType paymentType, string freeCode)
        {
            if (freeCode == "123456")
            {
                var payment = new PaymentMethod(freeCode,paymentType);
                _paymentMethods.Add(payment);
            }
        }
    }
}
