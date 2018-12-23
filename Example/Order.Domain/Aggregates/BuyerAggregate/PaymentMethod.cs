using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;

namespace Order.Domain.Aggregates.BuyerAggregate
{
    public class PaymentMethod : Entity<string>
    {
        public string FreeCode { get; }
        
        public PaymentType PaymentType { get; }

        public PaymentMethod() { }
        public PaymentMethod(string freeCode, PaymentType paymentType)
        {
            FreeCode = freeCode;
            PaymentType = paymentType;
        }
    }
}
