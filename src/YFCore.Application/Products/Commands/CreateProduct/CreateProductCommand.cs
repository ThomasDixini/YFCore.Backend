using System;

using MediatR;

namespace YFCore.Application.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        string Name,
        string Description,
        decimal PriceAmount,
        string PriceCurrency,
        Guid CategoryId
    ) : IRequest<Guid>;
}
