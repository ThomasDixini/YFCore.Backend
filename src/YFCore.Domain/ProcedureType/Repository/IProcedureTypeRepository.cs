using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.ProcedureTypes.Entity;
using YFCore.Domain.Shared.Repository;

namespace YFCore.Domain.ProcedureTypes.Repository
{
    public interface IProcedureTypeRepository : IRepository<ProcedureType>
    {
    }
}