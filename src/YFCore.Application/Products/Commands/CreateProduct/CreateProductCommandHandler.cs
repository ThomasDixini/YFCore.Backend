using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Domain.ProductEntity;
using YFCore.Domain.ProductRepository;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.CategoryId);
            product.ChangePrice(new Money(request.PriceAmount, request.PriceCurrency));
            _productRepository.Add(product);
            await _unitOfWork.CommitAsync(cancellationToken);
            return product.Id;
        }
    }
}
