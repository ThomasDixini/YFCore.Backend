using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Domain.Categories.Entity;
using YFCore.Domain.Categories.Repository;

namespace YFCore.Application.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category(request.Name, request.Description);
            _categoryRepository.Add(newCategory);
            await _unitOfWork.CommitAsync();
            return newCategory.Id;
        }
    }
}