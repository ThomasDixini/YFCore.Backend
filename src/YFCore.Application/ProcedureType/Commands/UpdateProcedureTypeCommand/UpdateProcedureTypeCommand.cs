using MediatR;

namespace YFCore.Application.ProcedureTypes.Commands.UpdateProcedureTypeCommand
{
    public sealed record UpdateProcedureTypeCommand
    (
        Guid Id,
        string? Name,
        string? Description,
        bool? isActive
    ) : IRequest<object>;
}