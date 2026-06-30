using System;

using FluentAssertions;

using Xunit;

using YFCore.Domain.ProductEntity;
using YFCore.Domain.Shared.Exceptions;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.Tests.Products
{
    public class ProductTests
    {
        [Fact]
        public void ProductConstructor_ShouldInitializeProperties_WhenValidDataIsProvided()
        {
            Guid categoryId = Guid.NewGuid();
            var product = new Product("Test Product", "A product description.", categoryId);

            product.Should().NotBeNull();
            product.Name.Should().Be("TEST PRODUCT");
            product.Description.Should().Be("A PRODUCT DESCRIPTION.");
            product.Price.Should().NotBeNull();
            product.CategoryId.Should().Be(categoryId);
            product.Active.Should().BeFalse();
        }

        [Fact]
        public void ProductValidate_ShouldThrow_WhenNameIsEmpty()
        {
            var price = new Money(10m, "USD");
            Action act = () => new Product("", "Description", Guid.NewGuid());

            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty.");
        }

        [Fact]
        public void ProductValidate_ShouldThrow_WhenDescriptionIsTooLong()
        {
            string longDescription = new string('a', 201);
            Action act = () => new Product("Name", longDescription, Guid.NewGuid());

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void ProductValidate_ShouldThrow_WhenCategoryIdIsEmpty()
        {
            Action act = () => new Product("Name", "Description", Guid.Empty);

            act.Should().Throw<ArgumentException>().WithMessage("CategoryId cannot be empty.");
        }

        [Fact]
        public void ChangeName_ShouldUpdateNameToUppercase()
        {
            var price = new Money(10m, "USD");
            var product = new Product("Test Product", "Description", Guid.NewGuid());

            product.ChangeName("updated product");

            product.Name.Should().Be("UPDATED PRODUCT");
        }

        [Fact]
        public void ChangeDescription_ShouldUpdateDescriptionToUppercase_WhenValidLength()
        {
            var price = new Money(10m, "USD");
            var product = new Product("Test Product", "Description", Guid.NewGuid());

            product.ChangeDescription("updated description");

            product.Description.Should().Be("UPDATED DESCRIPTION");
        }

        [Fact]
        public void ChangeDescription_ShouldThrow_WhenDescriptionIsTooLong()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());
            string longDescription = new string('a', 201);

            Action act = () => product.ChangeDescription(longDescription);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void ChangePrice_ShouldUpdatePrice_WhenValid()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());
            var newPrice = new Money(20m, "USD");

            product.ChangePrice(newPrice);

            product.Price.Should().Be(newPrice);
        }

        [Fact]
        public void ChangePrice_ShouldThrow_WhenCurrencyIsNull()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());
            var invalidPrice = new Money(20m, null!);

            Action act = () => product.ChangePrice(invalidPrice);

            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'Currency')");
        }

        [Fact]
        public void ChangePrice_ShouldThrow_WhenAmountIsNegative()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());
            var invalidPrice = new Money(-5m, "USD");

            Action act = () => product.ChangePrice(invalidPrice);

            act.Should().Throw<AmountNegativeException>().WithMessage("Amount cannot be negative.");
        }

        [Fact]
        public void Activate_ShouldSetActiveTrue()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());

            product.Activate();

            product.Active.Should().BeTrue();
        }

        [Fact]
        public void Deactivate_ShouldSetActiveFalse()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());

            product.Deactivate();

            product.Active.Should().BeFalse();
        }

        [Fact]
        public void MoneyToString_ShouldReturnCurrencySymbol_ForBrl()
        {
            var money = new Money(1234.5m, "BRL");

            money.Format().ToString().Should().Be("R$1,234.50");
        }

        [Fact]
        public void MoneyFormat_ShouldReturnCurrencySymbol_ForUsd()
        {
            var money = new Money(1234.5m, "USD");

            money.Format().Should().Be("$1,234.50");
        }

        [Fact]
        public void MoneyFormat_ShouldReturnCurrencySymbol_ForEur()
        {
            var money = new Money(1234.5m, "EUR");

            money.Format().Should().Be("€1,234.50");
        }

        [Fact]
        public void MoneyFormat_ShouldFallbackToCurrencyCode_ForUnknownCurrency()
        {
            var money = new Money(1234.5m, "XYZ");

            money.Format().Should().Be("1,234.50 XYZ");
        }

        [Fact]
        public void MoneyFormat_ShouldReturnCurrencySymbol_ForGbp()
        {
            var money = new Money(1234.5m, "GBP");

            money.Format().Should().Be("£1,234.50");
        }

        [Fact]
        public void MoneyFormat_ShouldReturnCurrencySymbol_ForJpy()
        {
            var money = new Money(1234m, "JPY");

            money.Format().Should().Be("¥1,234");
        }

        [Fact]
        public void MoneyFormat_ShouldReturnCurrencySymbol_ForAud()
        {
            var money = new Money(1234.5m, "AUD");

            money.Format().Should().Be("A$1,234.50");
        }

        [Fact]
        public void MoneyFormat_ShouldReturnCurrencySymbol_ForCad()
        {
            var money = new Money(1234.5m, "CAD");

            money.Format().Should().Be("C$1,234.50");
        }

        [Fact]
        public void MoneyToString_ShouldReturnAmountAndCurrency()
        {
            var money = new Money(1234.5m, "USD");

            money.ToString().Should().Be("1234.50 USD");
        }

        [Fact]
        public void ChangeCategory_ShouldUpdateCategoryId()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());
            var newCategoryId = Guid.NewGuid();

            product.ChangeCategory(newCategoryId);

            product.CategoryId.Should().Be(newCategoryId);
        }
        [Fact]
        public void ChangeCategory_ShouldThrowException_WhenCategoryIdIsEmpty()
        {
            var product = new Product("Test Product", "Description", Guid.NewGuid());
            var invalidCategoryId = Guid.Empty;

            Action act = () => product.ChangeCategory(invalidCategoryId);

            act.Should().Throw<ArgumentException>().WithMessage("CategoryId cannot be empty.");
        }
    }
}
