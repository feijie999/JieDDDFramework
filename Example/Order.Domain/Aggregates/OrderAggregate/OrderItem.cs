using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;

namespace Order.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity<string>
    {
        public string ProductId { get; }
        public string ProductName { get; }
        public int ProductCount { get; }
        public decimal ProductPrice { get; }

        protected OrderItem() { }
        public OrderItem(string productId, string productName, int productCount, decimal productPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductCount = productCount;
            ProductPrice = productPrice;
        }
    }
}
