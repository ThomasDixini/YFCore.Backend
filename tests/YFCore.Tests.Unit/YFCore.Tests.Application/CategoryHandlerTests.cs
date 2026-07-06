using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YFCore.Application.Shared.Events;
using YFCore.Application.Categories.Commands.CreateCategoryCommand;
using YFCore.Application.Categories.Commands.UpdateCategory;
using YFCore.Application.Categories.Handler;
using YFCore.Application.Categories.Queries.Dtos;
using YFCore.Domain.Categories.Events;
using YFCore.Application.Categories.Queries.GetCategoryById;
using YFCore.Application.Categories.Queries.GetAllCategories;
using YFCore.Application.Contracts;
using YFCore.Domain.Categories.Entity;
using YFCore.Domain.Categories.Repository;
using YFCore.Domain.ProductRepository;
using YFCore.Domain.Shared.Exceptions;
using YFCore.Domain.ProductEntity;

namespace YFCore.Tests.Unit.YFCore.Tests.Application
{
    public class CategoryHandlerTests
    {
        [Fact]
        public async Task CreateCategoryCommandHandler_ShouldAddCategoryAndCommit()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new CreateCategoryCommandHandler(categoryRepository.Object, unitOfWork.Object);
            var result = await handler.Handle(new CreateCategoryCommand("Test", "Description"), CancellationToken.None);

            categoryRepository.Verify(r => r.Add(It.IsAny<Category>()), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryCommandHandler_ShouldUpdateCategoryAndCommit()
        {
            var category = new Category("Test", "Description");
            var categoryRepository = new Mock<ICategoryRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            categoryRepository.Setup(r => r.GetByIdAsync(category.Id)).ReturnsAsync(category);
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new UpdateCategoryCommandHandler(categoryRepository.Object, unitOfWork.Object);
            var request = new UpdateCategoryCommand(category.Id, "Updated", "Updated description", false);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Id.Should().Be(category.Id);
            result.Name.Should().Be("UPDATED");
            result.Description.Should().Be("UPDATED DESCRIPTION");
            category.Active.Should().BeFalse();
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryCommandHandler_ShouldThrow_WhenCategoryNotFound()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            categoryRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Category?)null);

            var handler = new UpdateCategoryCommandHandler(categoryRepository.Object, unitOfWork.Object);
            Func<Task> act = async () => await handler.Handle(new UpdateCategoryCommand(Guid.NewGuid(), "Name", "Description", true), CancellationToken.None);

            await act.Should().ThrowAsync<DomainException>().WithMessage("Category not found");
        }

        [Fact]
        public async Task GetCategoryByIdQueryHandler_ShouldReturnCategory_WhenFound()
        {
            var category = new Category("Test", "Description");
            var repository = new Mock<ICategoryRepository>();
            repository.Setup(r => r.GetByIdAsync(category.Id)).ReturnsAsync(category);

            var handler = new GetCategoryByIdQueryHandler(repository.Object);
            var result = await handler.Handle(new GetCategoryByIdQuery(category.Id), CancellationToken.None);

            result.Should().BeSameAs(category);
        }

        [Fact]
        public async Task GetCategoryByIdQueryHandler_ShouldReturnNull_WhenNotFound()
        {
            var repository = new Mock<ICategoryRepository>();
            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Category?)null);

            var handler = new GetCategoryByIdQueryHandler(repository.Object);
            var result = await handler.Handle(new GetCategoryByIdQuery(Guid.NewGuid()), CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllCategoriesHandler_ShouldReturnCategoryDtos()
        {
            var repository = new Mock<ICategoryRepository>();
            var expected = new[] { new CategoryDto(Guid.NewGuid(), "TEST", "DESCRIPTION") };
            repository.Setup(r => r.GetAllAsync<CategoryDto>(It.IsAny<Expression<Func<Category, CategoryDto>>>() )).ReturnsAsync(expected);

            var handler = new GetAllCategoriesHandler(repository.Object);
            var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DeactivateCategoryHandler_ShouldDeactivateProductsAndCommit()
        {
            var product1 = new Product("Product 1", "Description", Guid.NewGuid());
            var product2 = new Product("Product 2", "Description", Guid.NewGuid());
            var repository = new Mock<IProductRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            repository.Setup(r => r.GetAllByCategoryIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Product> { product1, product2 });

            var handler = new DeactivateCategoryHandler(repository.Object, unitOfWork.Object);

            await handler.Handle(new DomainEventNotification<CategoryDeactivated>(new CategoryDeactivated(Guid.NewGuid())), CancellationToken.None);

            product1.Active.Should().BeFalse();
            product2.Active.Should().BeFalse();
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
