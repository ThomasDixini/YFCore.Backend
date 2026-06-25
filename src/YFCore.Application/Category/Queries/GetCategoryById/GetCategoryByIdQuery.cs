using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Domain.Categories.Entity;

namespace YFCore.Application.Categories.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(Guid id) : IRequest<object>;
}