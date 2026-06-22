using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Application.ProcedureType.Queries.GetProcedureTypeById;

namespace YFCore.Application.ProcedureType.Contracts
{
    public interface IProcedureTypeRead
    {
        Task<ProcedureTypeDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProcedureTypeDTO>> GetProcedureTypesAsync();
    }
}