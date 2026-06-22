using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Application.ProcedureType.Queries.GetProcedureTypeById
{
    public record ProcedureTypeDTO(Guid Id, string Name, string Description);
}
