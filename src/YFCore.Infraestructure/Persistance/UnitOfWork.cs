using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Application.Contracts;
using YFCore.Domain.Shared.Base;
using YFCore.Infraestructure.EventHandler;

namespace YFCore.Infraestructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IDomainEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
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
                await _dispatcher.Dispatch(domainEvent, cancellationToken);
            }

            return success;
        }
    }
}