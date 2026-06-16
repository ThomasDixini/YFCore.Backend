using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Application.Contracts;
using YFCore.Domain.Categories.Events;
using YFCore.Domain.ProductRepository;

namespace YFCore.Application.Categories.Handler
{
    public class DeactivateCategoryHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeactivateCategoryHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CategoryDeactivated categoryDeactivated)
        {
            var products = await _productRepository.GetAllByCategoryIdAsync(categoryDeactivated.CategoryId);
            foreach (var product in products)
            {
                product.Deactivate();
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
