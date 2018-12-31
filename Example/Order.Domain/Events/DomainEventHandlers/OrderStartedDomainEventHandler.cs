using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JieDDDFramework.Core.Exceptions.Utilities;
using JieDDDFramework.Data.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain.Aggregates.BuyerAggregate;

namespace Order.Domain.Events.DomainEventHandlers
{
    public class OrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IRepositoryBase<Buyer> _buyerRepository;

        public OrderStartedDomainEventHandler(IRepositoryBase<Buyer> buyerRepository, ILoggerFactory logger)
        {
            _buyerRepository = Check.NotNull(buyerRepository, nameof(buyerRepository));
            _logger = Check.NotNull(logger, nameof(logger)); ;
        }

        public async Task Handle(OrderStartedDomainEvent orderStartedEvent, CancellationToken cancellationToken)
        {
            var buyer = await _buyerRepository.FindEntityAsync(orderStartedEvent.UserId);
            var buyerOriginallyExisted = buyer != null;

            if (!buyerOriginallyExisted)
            {
                buyer = new Buyer(orderStartedEvent.UserId, orderStartedEvent.UserName);
            }

            buyer.VerifyOrAddPaymentMethod(null,
                "123456",orderStartedEvent.Order.Id);
            if (buyerOriginallyExisted)
            {
                _buyerRepository.Update(buyer);
            }
            else
            {
                await _buyerRepository.InsertAsync(buyer, cancellationToken);
            }
            await _buyerRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}
