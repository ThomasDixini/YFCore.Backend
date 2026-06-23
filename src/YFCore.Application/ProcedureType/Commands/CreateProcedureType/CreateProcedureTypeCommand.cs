using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

namespace YFCore.Application.ProcedureTypes.Commands.CreateProcedureType
{
    public sealed record CreateProcedureTypeCommand
    (
        string Name,
        string Description
    ) : IRequest<Guid>;
}