using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

namespace YFCore.Application.ProcedureTypes.Queries.GetProcedureTypeById
{
    public record GetProcedureTypeByIdQuery(Guid Id) : IRequest<ProcedureTypeDTO>
    {
    }
}
