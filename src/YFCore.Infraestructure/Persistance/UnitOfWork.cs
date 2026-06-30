using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Application.Shared.Events;
using YFCore.Domain.Shared.Base;

namespace YFCore.Infraestructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = _context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(c => c.Entity.DomainEvents != null && c.Entity.DomainEvents.Any())
                .ToList();
            var domainEvents = domainEntities
                .SelectMany(c => c.Entity.DomainEvents)
                .ToList();

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!success)
                return false;

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                var wrapperType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
                var wrapperInstance = Activator.CreateInstance(wrapperType, domainEvent);
                if (wrapperInstance is INotification mediatrNotification)
                {
                    await _mediator.Publish(mediatrNotification, cancellationToken);
                }
            }

            return success;
        }
    }
}