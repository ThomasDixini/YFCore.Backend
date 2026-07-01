using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Products.Queries.Dtos;
using YFCore.Domain.ProductRepository;

namespace YFCore.Application.Products.Queries.ListProducts
{
    public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;

        public ListProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(product => new ProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.Price.Amount,
                product.Price.Currency,
                product.Active,
                product.CategoryId
            ));
        }
    }
}
