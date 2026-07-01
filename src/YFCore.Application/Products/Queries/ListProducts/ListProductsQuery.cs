using System.Collections.Generic;

using MediatR;

using YFCore.Application.Products.Queries.Dtos;

namespace YFCore.Application.Products.Queries.ListProducts
{
    public sealed class ListProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }
}
