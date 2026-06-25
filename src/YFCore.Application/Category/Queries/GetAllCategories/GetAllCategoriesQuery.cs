using System.Collections.Generic;

using MediatR;

using YFCore.Application.Categories.Queries.Dtos;

namespace YFCore.Application.Categories.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
}
