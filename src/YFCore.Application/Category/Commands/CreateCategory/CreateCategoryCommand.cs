using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

namespace YFCore.Application.Categories.Commands.CreateCategoryCommand
{
    public sealed record CreateCategoryCommand
    (
        string Name,
        string Description
    ) : IRequest<Guid>;
}