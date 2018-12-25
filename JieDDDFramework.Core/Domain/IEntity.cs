using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace JieDDDFramework.Core.Domain
{
    public interface IEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        void AddDomainEvent(INotification eventItem);

        void RemoveDomainEvent(INotification eventItem);

        void ClearDomainEvents();
    }
}
