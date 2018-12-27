using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;
using JieDDDFramework.Core.Exceptions;

namespace Order.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity<string>
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int ProductCount { get; private set; }
        public decimal ProductPrice { get; private set; }
        public string OrderId { get; private set; }
        public virtual Order Order { get; private set; }

        protected OrderItem() { }
        public OrderItem(string productId, string productName, int productCount, decimal productPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductCount = productCount;
            ProductPrice = productPrice;
        }

        public void SetNewDiscount(decimal discount)
        {
            if (discount<0)
            {
                throw new DomainException("Discount is not valid");
            }

            ProductCount = (int) discount;
        }
    }
}
