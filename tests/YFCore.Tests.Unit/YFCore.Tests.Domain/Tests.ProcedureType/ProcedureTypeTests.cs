using System;

using FluentAssertions;
using Xunit;

using YFCore.Domain.ProcedureTypes.Entity;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsProcedureType
{
    public class ProcedureTypeTests
    {
        [Fact]
        public void ProcedureTypeConstructor_ShouldInitializeProperties_WhenValidDataIsProvided()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";

            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.Should().NotBeNull();
            procedureType.Name.Should().Be("CONSULTATION");
            procedureType.Description.Should().Be("A MEDICAL CONSULTATION APPOINTMENT.");
            procedureType.Price.Amount.Should().Be(0m);
            procedureType.Price.Currency.Should().Be("USD");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenNameIsEmpty()
        {
            string description = "A medical consultation appointment.";

            Action act = () => new ProcedureType("", description);

            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty.");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenNameIsNull()
        {
            string description = "A medical consultation appointment.";

            Action act = () => new ProcedureType(null, description);

            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty.");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenNameIsWhitespace()
        {
            string description = "A medical consultation appointment.";

            Action act = () => new ProcedureType("   ", description);

            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty.");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenDescriptionIsNull()
        {
            string name = "Consultation";

            Action act = () => new ProcedureType(name, null!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenDescriptionIsTooLong()
        {
            string name = "Consultation";
            string longDescription = new string('a', 201);

            Action act = () => new ProcedureType(name, longDescription);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void ProcedureTypeName_ShouldBe_Updated()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.ChangeName("Surgery");

            procedureType.Name.Should().Be("SURGERY");
        }

        [Fact]
        public void ProcedureTypeDescription_ShouldBe_Updated()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.ChangeDescription("A surgical procedure.");

            procedureType.Description.Should().Be("A SURGICAL PROCEDURE.");
        }

        [Fact]
        public void ProcedureTypeDescription_ShouldThrowException_WhenLengthExceedsMaximum()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);
            string newDescription = new string('a', 201);

            Action act = () => procedureType.ChangeDescription(newDescription);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void ProcedureTypeDescription_ShouldThrowException_WhenDescriptionIsNull()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);

            Action act = () => procedureType.ChangeDescription(null!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ProcedureTypePrice_ShouldBe_Updated()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);
            Money newPrice = new Money(200.00m, "BRL");

            procedureType.ChangePrice(newPrice);

            procedureType.Price.Should().Be(newPrice);
            procedureType.Price.Amount.Should().Be(200.00m);
            procedureType.Price.Currency.Should().Be("BRL");
        }

        [Fact]
        public void ProcedureTypeChangePrice_ShouldThrowException_WhenPriceIsNull()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);

            Action act = () => procedureType.ChangePrice(null!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ProcedureTypeChangePrice_ShouldThrowException_WhenNegative()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);
            Money negativePrice = new Money(-100.00m, "USD");

            Action act = () => procedureType.ChangePrice(negativePrice);

            act.Should().Throw<ArgumentException>().WithMessage("Price cannot be negative.");
        }

        [Fact]
        public void ProcedureTypeChangePrice_ShouldThrowException_WhenCurrencyIsNull()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);
            Money invalidPrice = new Money(200.00m, null);

            Action act = () => procedureType.ChangePrice(invalidPrice);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ProcedureType_ShouldAllowZeroPrice()
        {
            string name = "Free Consultation";
            string description = "A free medical consultation.";
            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.Price.Amount.Should().Be(0m);
            procedureType.Price.Currency.Should().Be("USD");
        }

        [Fact]
        public void ProcedureType_ShouldAllowMaxLengthDescription()
        {
            string name = "Consultation";
            string description = new string('a', 200);
            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.Description.Length.Should().Be(200);
        }

        [Fact]
        public void ProcedureType_ShouldTrimWhitespace_InName()
        {
            string name = "   Consultation   ";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.Name.Should().Be("   CONSULTATION   ");
        }

        [Fact]
        public void ProcedureType_ShouldTrimWhitespace_InDescription()
        {
            string name = "Consultation";
            string description = "   A medical consultation appointment.   ";
            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.Description.Should().Be("   A MEDICAL CONSULTATION APPOINTMENT.   ");
        }

        [Fact]
        public void ProcedureType_ShouldSupportMultipleCurrencies()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            ProcedureType procedureType = new ProcedureType(name, description);

            procedureType.ChangePrice(new Money(150.00m, "USD"));
            procedureType.Price.Currency.Should().Be("USD");

            procedureType.ChangePrice(new Money(130.00m, "EUR"));
            procedureType.Price.Currency.Should().Be("EUR");

            procedureType.ChangePrice(new Money(750.00m, "BRL"));
            procedureType.Price.Currency.Should().Be("BRL");
        }
    }
}
