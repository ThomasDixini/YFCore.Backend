using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Domain.Shared.Base
{
    public class DomainEventBase : IDomainEvent
    {
        public Guid Id { get; }

        public DateTime OccurredAt { get; }

        public DomainEventBase()
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.UtcNow;
        }
    }
}