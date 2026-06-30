using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using YFCore.Api.Controllers;
using YFCore.Application.Categories.Commands.CreateCategoryCommand;
using YFCore.Application.Categories.Commands.UpdateCategory;
using YFCore.Application.Categories.Queries.Dtos;
using YFCore.Application.Categories.Queries.GetAllCategories;
using YFCore.Application.Categories.Queries.GetCategoryById;
using YFCore.Domain.Categories.Entity;
namespace YFCore.Api.Controllers.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return OkResponse(categories, "Categories retrieved successfully.");
        }
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse<object>>> GetById(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            return OkResponse(category, "Category retrieved successfully.");
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create(CreateCategoryCommand command)
        {
            var categoryCreatedId = await _mediator.Send(command);
            return CreatedResponse(nameof(GetById), new { id = categoryCreatedId.ToString() }, categoryCreatedId.ToString(), "Category retrieved successfully.");
        }
        [HttpPatch]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> Update(UpdateCategoryCommand command)
        {
            var categoryUpdated = await _mediator.Send(command);
            return OkResponse(categoryUpdated, "Procedure type updated successfully.");
        }
    }


}
