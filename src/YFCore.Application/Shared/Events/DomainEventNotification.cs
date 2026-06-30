using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Domain.Shared.Base;

namespace YFCore.Application.Shared.Events
{
    public class DomainEventNotification<TEvent> : INotification where TEvent : IDomainEvent
    {
        public TEvent DomainEvent { get; }
        public DomainEventNotification(TEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}