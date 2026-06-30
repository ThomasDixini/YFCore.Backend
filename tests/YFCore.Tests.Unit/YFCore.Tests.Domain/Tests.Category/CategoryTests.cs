using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

using YFCore.Domain.Categories.Entity;
using YFCore.Domain.Categories.Events;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsCategory
{
    public class TestsCategory
    {
        [Fact]
        public void CategoryConstructor_ShouldInitializeProperties_WhenValidDataIsProvided()
        {
            string name = "Test Category";
            string description = "This is a test category.";

            Category category = new Category(name, description);

            category.Should().NotBeNull();
            category.Name.Should().Be("TEST CATEGORY");
            category.Description.Should().Be("THIS IS A TEST CATEGORY.");
            category.Active.Should().BeTrue();
        }

        [Fact]
        public void CategoryConstructor_ShouldThrow_WhenNameIsEmpty()
        {
            Guid categoryId = Guid.NewGuid();
            string description = "This is a test category.";

            Action act = () => new Category("", description);

            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty.");
        }

        [Fact]
        public void CategoryConstructor_ShouldThrow_WhenDescriptionIsTooLong()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "Test Category";
            string longDescription = new string('a', 201);

            Action act = () => new Category(name, longDescription);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void CategoryName_ShouldBe_Updated()
        {
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(name, description);

            category.ChangeName("Updated Category Name");

            category.Should().NotBeNull();
            category.Name.Should().Be("UPDATED CATEGORY NAME");
        }
        [Fact]
        public void CategoryDescription_ShouldBe_Updated()
        {
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(name, description);

            category.ChangeDescription("Updated Category Description");

            category.Should().NotBeNull();
            category.Description.Should().Be("UPDATED CATEGORY DESCRIPTION");
        }
        [Fact]
        public void CategoryDescription_ShouldThrowException_WhenLengthExceedsMaximum()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(name, description);
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
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(name, description);

            category.Deactivate();
            category.Activate();

            category.Active.Should().BeTrue();
            category.DomainEvents.Should().ContainSingle(e => e is CategoryActivated);
        }
        [Fact]
        public void CategoryDeactivation_ShouldSetActiveToFalse()
        {
            string name = "Test Category";
            string description = "This is a test category.";
            Category category = new Category(name, description);

            category.Deactivate();

            category.Active.Should().BeFalse();
            category.DomainEvents.Should().ContainSingle(e => e is CategoryDeactivated);
        }
    }
}
