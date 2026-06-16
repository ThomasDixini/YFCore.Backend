using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YFCore.Api.Controllers;
using YFCore.Application.Category.Queries.GetAllCategories;

namespace YFCore.Api.Controllers.Categories
{
    [Route("api/[controller]")]
    public class Categories : BaseController
    {
        private readonly IMediator _mediator;

        public Categories(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return OkResponse(categories, "Categories retrieved successfully.");
        }
    }


}