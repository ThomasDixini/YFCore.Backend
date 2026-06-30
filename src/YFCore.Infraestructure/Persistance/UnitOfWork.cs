using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
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
            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}