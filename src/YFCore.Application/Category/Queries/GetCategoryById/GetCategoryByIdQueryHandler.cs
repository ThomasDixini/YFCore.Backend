using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Categories.Queries.Dtos;
using YFCore.Domain.Categories.Entity;
using YFCore.Domain.Categories.Repository;

namespace YFCore.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, object?>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<object?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByIdAsync(request.id);
        }
    }
}