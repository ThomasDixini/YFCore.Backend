using System;

using MediatR;

namespace YFCore.Application.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        decimal PriceAmount,
        string PriceCurrency,
        Guid CategoryId,
        bool Active
    ) : IRequest<bool>;
}
