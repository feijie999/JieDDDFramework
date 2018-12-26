using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JieDDDFramework.Core.Exceptions.Utilities;
using JieDDDFramework.Data.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Order.Domain.Events.DomainEventHandlers
{
    public class UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler
        : INotificationHandler<BuyerAndPaymentMethodVerifiedDomainEvent>
    {
        private readonly IRepositoryBase<Aggregates.OrderAggregate.Order> _orderRepository;
        private readonly ILoggerFactory _logger;

        public UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler(
            IRepositoryBase<Aggregates.OrderAggregate.Order> orderRepository, ILoggerFactory logger)
        {
            _orderRepository = Check.NotNull(orderRepository, nameof(orderRepository));
            _logger = Check.NotNull(logger, nameof(logger));
        }
        public async Task Handle(BuyerAndPaymentMethodVerifiedDomainEvent buyerPaymentMethodVerifiedEvent, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.FindEntityAsync(buyerPaymentMethodVerifiedEvent.OrderId);
            orderToUpdate.SetBuyerId(buyerPaymentMethodVerifiedEvent.Buyer.Id);
            orderToUpdate.SetPaymentId(buyerPaymentMethodVerifiedEvent.Payment.Id);

            _logger.CreateLogger(nameof(UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler))
                .LogTrace($"Order with Id: {buyerPaymentMethodVerifiedEvent.OrderId} has been successfully updated with a payment method id: { buyerPaymentMethodVerifiedEvent.Payment.Id }");
        }
    }
}
