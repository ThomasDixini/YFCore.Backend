using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Application.Contracts
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}