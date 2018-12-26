using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JieDDDFramework.Data.Repository;
using MediatR;
using Order.Domain.Aggregates.OrderAggregate;

namespace Order.Domain.Application.Commands
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IRepositoryBase<Aggregates.OrderAggregate.Order> _orderRepository;
        private readonly IMediator _mediator;
        
        public CreateOrderCommandHandler(IMediator mediator, IRepositoryBase<Aggregates.OrderAggregate.Order> orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            var address = new Address(message.Street, message.City, message.State, message.Country, message.ZipCode);
            var order = new Aggregates.OrderAggregate.Order(message.UserId, message.UserName, address);

            foreach (var item in message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Discount, item.UnitPrice);
            }
            await _orderRepository.InsertAsync(order, cancellationToken);
            return await _orderRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}
