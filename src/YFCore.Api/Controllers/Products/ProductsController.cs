using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using YFCore.Application.Products.Commands.CreateProduct;
using YFCore.Application.Products.Commands.UpdateProduct;
using YFCore.Application.Products.Queries.GetProductById;
using YFCore.Application.Products.Queries.ListProducts;
using YFCore.Application.Products.Queries.Dtos;

namespace YFCore.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDTO>>>> ListProducts()
        {
            var products = await _mediator.Send(new ListProductsQuery());
            return OkResponse(products, "Products retrieved successfully.");
        }

        [HttpGet("{id:guid}", Name = nameof(GetProductById))]
        public async Task<ActionResult<ApiResponse<ProductDTO?>>> GetProductById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product is null)
                return ErrorResponse<ProductDTO?>(404, "Product not found.");

            return OkResponse(product, "Product retrieved successfully.");
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create(CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedResponse(nameof(GetProductById), new { id = productId }, productId.ToString(), "Product created successfully.");
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(Guid id, UpdateProductCommand command)
        {
            command = command with { Id = id };
            var result = await _mediator.Send(command);
            return OkResponse(result, "Product updated successfully.");
        }
    }
}