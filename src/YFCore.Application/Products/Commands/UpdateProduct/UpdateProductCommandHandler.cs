using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Application.Products.Commands.UpdateProduct;
using YFCore.Application.Products.Queries.Dtos;
using YFCore.Domain.ProductEntity;
using YFCore.Domain.ProductRepository;
using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.Exceptions;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id) ?? throw new DomainException("Product not found");

            if (!string.IsNullOrWhiteSpace(request.Name))
                product.ChangeName(request.Name);

            if (!string.IsNullOrWhiteSpace(request.Description))
                product.ChangeDescription(request.Description);

            if (request.PriceAmount.HasValue || request.PriceCurrency is not null)
            {
                var price = new Money(
                    request.PriceAmount ?? product.Price.Amount,
                    request.PriceCurrency ?? product.Price.Currency
                );
                product.ChangePrice(price);
            }

            if (request.CategoryId.HasValue)
                product.ChangeCategory(request.CategoryId.Value);

            if (request.Active.HasValue && request.Active.Value != product.Active)
            {
                if (request.Active.Value)
                    product.Activate();
                else
                    product.Deactivate();
            }

            _productRepository.Update(product);
            return await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
