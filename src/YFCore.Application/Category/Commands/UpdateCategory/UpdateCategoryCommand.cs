using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Categories.Queries.Dtos;

namespace YFCore.Application.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand
    (
        Guid id,
        string name,
        string description,
        bool active
    ) : IRequest<CategoryDto>;
}