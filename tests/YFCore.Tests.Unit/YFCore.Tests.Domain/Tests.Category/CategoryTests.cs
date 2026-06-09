using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

using YFCore.Domain.Categories.Entity;
using YFCore.Domain.Categories.Events;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsCategory
{
    public class TestsCategory
    {
        [Fact]
        public void CategoryName_ShouldBe_Updated()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(categoryId, name, description);

            category.ChangeName("Updated Category Name");

            category.Should().NotBeNull();
            category.Id.Should().Be(categoryId);
            category.Name.Should().Be("UPDATED CATEGORY NAME");
        }
        [Fact]
        public void CategoryDescription_ShouldBe_Updated()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(categoryId, name, description);

            category.ChangeDescription("Updated Category Description");

            category.Should().NotBeNull();
            category.Id.Should().Be(categoryId);
            category.Description.Should().Be("UPDATED CATEGORY DESCRIPTION");
        }
        [Fact]
        public void CategoryDescription_ShouldThrowException_WhenLengthExceedsMaximum()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(categoryId, name, description);
            string newDescription = @"This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.
            This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.
            This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.
            This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.
            This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.This is a test category. This is a test category. This is a test category. This is a test category. This is a test category. This is a test category.";

            Action act = () => category.ChangeDescription(newDescription);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void CategoryActivation_ShouldSetActiveToTrue()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(categoryId, name, description);

            category.Deactivate();
            category.Activate();

            category.Active.Should().BeTrue();
            category.DomainEvents.Should().ContainSingle(e => e is CategoryActivated && ((CategoryActivated)e).CategoryId == categoryId);
        }
        [Fact]
        public void CategoryDeactivation_ShouldSetActiveToFalse()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(categoryId, name, description);

            category.Deactivate();

            category.Active.Should().BeFalse();
            category.DomainEvents.Should().ContainSingle(e => e is CategoryDeactivated && ((CategoryDeactivated)e).CategoryId == categoryId);
        }
    }
}