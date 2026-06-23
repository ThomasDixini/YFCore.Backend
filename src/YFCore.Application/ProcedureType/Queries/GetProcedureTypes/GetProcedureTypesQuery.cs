using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.ProcedureTypes.Queries.GetProcedureTypeById;

namespace YFCore.Application.ProcedureTypes.Queries.GetProcedureTypes
{
    public class GetProcedureTypesQuery : IRequest<IEnumerable<ProcedureTypeDTO>>
    {
    }
}