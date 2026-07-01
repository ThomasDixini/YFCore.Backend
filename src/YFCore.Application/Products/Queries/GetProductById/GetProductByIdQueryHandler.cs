using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Products.Queries.Dtos;
using YFCore.Domain.ProductRepository;

namespace YFCore.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product is null)
                return null;

            return new ProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.Price.Amount,
                product.Price.Currency,
                product.Active,
                product.CategoryId
            );
        }
    }
}
