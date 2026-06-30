using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Categories.Commands.UpdateCategory;
using YFCore.Application.Categories.Queries.Dtos;
using YFCore.Application.Contracts;
using YFCore.Domain.Categories.Repository;
using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.Exceptions;

namespace YFCore.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.id) ?? throw new DomainException("Category not found");
            category.UpdateNameAndDescription(request.name, request.description);

            if (request.active != category.Active)
            {
                if (request.active is true)
                    category.Activate();
                else
                    category.Deactivate();
            }

            await _unitOfWork.CommitAsync();

            return new CategoryDto(category.Id, category.Name, category.Description);
        }
    }
}