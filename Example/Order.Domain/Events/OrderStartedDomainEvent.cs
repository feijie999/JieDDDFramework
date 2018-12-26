using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Order.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {
        public string UserId { get; }
        public string UserName { get; }
        public Aggregates.OrderAggregate.Order Order { get; }

        public OrderStartedDomainEvent(Aggregates.OrderAggregate.Order order, string userId, string userName)
        {
            Order = order;
            UserId = userId;
            UserName = userName;
        }
    }
}
