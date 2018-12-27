using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JieDDDFramework.Core.Domain;
using JieDDDFramework.Core.EntitySpecifications;
using Order.Domain.Aggregates.BuyerAggregate;
using Order.Domain.Events;

namespace Order.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity<string>, IAggregateRoot, ICreatedTimeState,IDeletedState
    {
        protected Order()
        {
            CreatedTime = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }
        public Address Address { get; private set; }

        public int? OrderStatusId { get; private set; }
        public virtual OrderStatus OrderStatus { get; private set; }

        public string BuyerId { get; private set; }
        public virtual Buyer Buyer { get; private set; }

        public string PaymentMethodId { get; private set; }
        public virtual PaymentMethod PaymentMethod { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime? DeletedTime { get; private set; }
        private List<OrderItem> _orderItems;
        public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(string userId, string userName, Address address,string buyerId = null, string paymentMethodId = null) : this()
        {
            BuyerId = buyerId;
            PaymentMethodId = paymentMethodId;
            OrderStatusId = OrderStatus.Submitted.Id;
            Address = address;

            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName);

            this.AddDomainEvent(orderStartedDomainEvent);
        }

        public  void AddOrderItem(string productId, string productName, decimal unitPrice, decimal discount)
        {
            var existingOrderForProduct = _orderItems
                .SingleOrDefault(o => o.Id == productId);

            if (existingOrderForProduct != null)
            {

                if (discount > existingOrderForProduct.ProductCount)
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }
            }
            else
            {

                var orderItem = new OrderItem(productId, productName, (int)discount, unitPrice);
                _orderItems.Add(orderItem);
            }
        }

        public void SetPaymentId(string id)
        {
            PaymentMethodId = id;
        }

        public void SetBuyerId(string id)
        {
            BuyerId = id;
        }


        public void Delete()
        {
            Deleted = true;
            DeletedTime = DateTime.Now;
        }
    
    }
}
