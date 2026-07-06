using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YFCore.Application.Contracts;
using YFCore.Application.Products.Commands.CreateProduct;
using YFCore.Application.Products.Commands.UpdateProduct;
using YFCore.Application.Products.Queries.GetProductById;
using YFCore.Application.Products.Queries.ListProducts;
using YFCore.Application.Products.Queries.Dtos;
using YFCore.Domain.ProductEntity;
using YFCore.Domain.ProductRepository;
using YFCore.Domain.Shared.Exceptions;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Application
{
    public class ProductHandlerTests
    {
        [Fact]
        public async Task CreateProductCommandHandler_ShouldAddAndCommit()
        {
            var productRepository = new Mock<IProductRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new CreateProductCommandHandler(productRepository.Object, unitOfWork.Object);
            var request = new CreateProductCommand("Test Product", "Description", 15m, "USD", Guid.NewGuid());

            var result = await handler.Handle(request, CancellationToken.None);

            productRepository.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProductCommandHandler_ShouldUpdateAndCommit()
        {
            var categoryId = Guid.NewGuid();
            var product = new Product("Test", "Description", categoryId);
            var repository = new Mock<IProductRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new UpdateProductCommandHandler(repository.Object, unitOfWork.Object);
            var request = new UpdateProductCommand(product.Id, "Updated", "Updated description", 25m, "USD", Guid.NewGuid(), true);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().BeTrue();
            product.Name.Should().Be("UPDATED");
            product.Description.Should().Be("UPDATED DESCRIPTION");
            product.Price.Amount.Should().Be(25m);
            product.Active.Should().BeTrue();
            repository.Verify(r => r.Update(product), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProductCommandHandler_ShouldThrow_WhenNotFound()
        {
            var repository = new Mock<IProductRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            var handler = new UpdateProductCommandHandler(repository.Object, unitOfWork.Object);
            Func<Task> act = async () => await handler.Handle(new UpdateProductCommand(Guid.NewGuid(), "Name", "Description", 10m, "USD", Guid.NewGuid(), true), CancellationToken.None);

            await act.Should().ThrowAsync<DomainException>().WithMessage("Product not found");
        }

        [Fact]
        public async Task GetProductByIdQueryHandler_ShouldReturnDto_WhenFound()
        {
            var product = new Product("Test", "Description", Guid.NewGuid());
            var repository = new Mock<IProductRepository>();
            repository.Setup(r => r.GetByIdWithCategoryAsync(product.Id)).ReturnsAsync(product);

            var handler = new GetProductByIdQueryHandler(repository.Object);
            var result = await handler.Handle(new GetProductByIdQuery(product.Id), CancellationToken.None);

            result.Should().NotBeNull();
            result!.Id.Should().Be(product.Id);
            result.CategoryName.Should().BeEmpty();
        }

        [Fact]
        public async Task GetProductByIdQueryHandler_ShouldReturnNull_WhenNotFound()
        {
            var repository = new Mock<IProductRepository>();
            repository.Setup(r => r.GetByIdWithCategoryAsync(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            var handler = new GetProductByIdQueryHandler(repository.Object);
            var result = await handler.Handle(new GetProductByIdQuery(Guid.NewGuid()), CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task ListProductsQueryHandler_ShouldReturnDtos()
        {
            var product = new Product("Test", "Description", Guid.NewGuid());
            var repository = new Mock<IProductRepository>();
            repository.Setup(r => r.GetAllWithCategoryAsync()).ReturnsAsync(new List<Product> { product });

            var handler = new ListProductsQueryHandler(repository.Object);
            var result = await handler.Handle(new ListProductsQuery(), CancellationToken.None);

            result.Should().ContainSingle();
            result.Single().Id.Should().Be(product.Id);
        }
    }
}
