using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.ProcedureType.Queries.GetProcedureTypeById;

namespace YFCore.Application.ProcedureType.Queries.GetProcedureTypes
{
    public class GetProcedureTypesQuery : IRequest<IEnumerable<ProcedureTypeDTO>>
    {
    }
}