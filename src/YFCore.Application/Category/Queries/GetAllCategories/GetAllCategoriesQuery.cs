using System.Collections.Generic;

using MediatR;

namespace YFCore.Application.Category.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
}
