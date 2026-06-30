using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Application.Shared.Events;
using YFCore.Domain.Categories.Events;
using YFCore.Domain.ProductRepository;

namespace YFCore.Application.Categories.Handler
{
    public class DeactivateCategoryHandler : INotificationHandler<DomainEventNotification<CategoryDeactivated>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeactivateCategoryHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DomainEventNotification<CategoryDeactivated> notification, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllByCategoryIdAsync(notification.DomainEvent.CategoryId);
            foreach (var product in products)
            {
                product.Deactivate();
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
