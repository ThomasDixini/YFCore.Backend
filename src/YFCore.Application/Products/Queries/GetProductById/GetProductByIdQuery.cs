using System;

using MediatR;

using YFCore.Application.Products.Queries.Dtos;

namespace YFCore.Application.Products.Queries.GetProductById
{
    public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDTO?>;
}
