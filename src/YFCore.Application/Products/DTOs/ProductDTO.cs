using System;

namespace YFCore.Application.Products.Queries.Dtos
{
    public sealed record ProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal PriceAmount,
        string PriceCurrency,
        bool Active,
        Guid CategoryId,
        string CategoryName
    );
}
