using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

using YFCore.Domain.ProcedureType.Entity;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsProcedureType
{
    public class ProcedureTypeTests
    {
        private UnavailableTimeSlots CreateDefaultUnavailableTimeSlots()
        {
            DateOnly futureDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            Date date = new Date(futureDate);
            return new UnavailableTimeSlots(date, new[] { new TimeOnly(10, 0) });
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldInitializeProperties_WhenValidDataIsProvided()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);

            procedureType.Should().NotBeNull();
            procedureType.Name.Should().Be("CONSULTATION");
            procedureType.Description.Should().Be("A MEDICAL CONSULTATION APPOINTMENT.");
            procedureType.Price.Should().Be(price);
            procedureType.Price.Amount.Should().Be(150.00m);
            procedureType.Price.Currency.Should().Be("USD");
            procedureType.UnavailableTimeSlots.Should().Be(unavailableTimeSlots);
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenNameIsEmpty()
        {
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            Action act = () => new ProcedureType("", description, price, unavailableTimeSlots);

            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty.");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenNameIsNull()
        {
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            Action act = () => new ProcedureType(null, description, price, unavailableTimeSlots);

            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty.");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenDescriptionIsTooLong()
        {
            string name = "Consultation";
            string longDescription = new string('a', 201);
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            Action act = () => new ProcedureType(name, longDescription, price, unavailableTimeSlots);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenPriceIsNegative()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(-50.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            Action act = () => new ProcedureType(name, description, price, unavailableTimeSlots);

            act.Should().Throw<ArgumentException>().WithMessage("Price cannot be negative.");
        }

        [Fact]
        public void ProcedureTypeConstructor_ShouldThrow_WhenPriceCurrencyIsNull()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, null);
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            Action act = () => new ProcedureType(name, description, price, unavailableTimeSlots);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ProcedureTypeName_ShouldBe_Updated()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);

            procedureType.ChangeName("Surgery");

            procedureType.Name.Should().Be("SURGERY");
        }

        [Fact]
        public void ProcedureTypeDescription_ShouldBe_Updated()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);

            procedureType.ChangeDescription("A surgical procedure.");

            procedureType.Description.Should().Be("A SURGICAL PROCEDURE.");
        }

        [Fact]
        public void ProcedureTypeDescription_ShouldThrowException_WhenLengthExceedsMaximum()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);
            string newDescription = new string('a', 201);

            Action act = () => procedureType.ChangeDescription(newDescription);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be longer than 200 characters.");
        }

        [Fact]
        public void ProcedureTypePrice_ShouldBe_Updated()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);
            Money newPrice = new Money(200.00m, "BRL");

            procedureType.ChangePrice(newPrice);

            procedureType.Price.Should().Be(newPrice);
            procedureType.Price.Amount.Should().Be(200.00m);
            procedureType.Price.Currency.Should().Be("BRL");
        }

        [Fact]
        public void ProcedureTypePrice_ShouldThrowException_WhenNegative()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);
            Money negativePrice = new Money(-100.00m, "USD");

            Action act = () => procedureType.ChangePrice(negativePrice);

            act.Should().Throw<ArgumentException>().WithMessage("Price cannot be negative.");
        }

        [Fact]
        public void ProcedureTypePrice_ShouldThrowException_WhenCurrencyIsNull()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);
            Money invalidPrice = new Money(200.00m, null);

            Action act = () => procedureType.ChangePrice(invalidPrice);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ProcedureType_ShouldAllowZeroPrice()
        {
            string name = "Free Consultation";
            string description = "A free medical consultation.";
            Money price = new Money(0m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);

            procedureType.Price.Amount.Should().Be(0m);
        }

        [Fact]
        public void ProcedureType_ShouldAllowMaxLengthDescription()
        {
            string name = "Consultation";
            string description = new string('a', 200);
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);

            procedureType.Description.Length.Should().Be(200);
        }

        [Fact]
        public void ProcedureType_ShouldTrimWhitespace_InName()
        {
            string name = "   Consultation   ";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);

            procedureType.Name.Should().Be("   CONSULTATION   ");
        }

        [Fact]
        public void ProcedureType_ShouldTrimWhitespace_InDescription()
        {
            string name = "Consultation";
            string description = "   A medical consultation appointment.   ";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableTimeSlots);

            procedureType.Description.Should().Be("   A MEDICAL CONSULTATION APPOINTMENT.   ");
        }

        [Fact]
        public void ProcedureType_ShouldSupportMultipleCurrencies()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money priceUSD = new Money(150.00m, "USD");
            Money priceEUR = new Money(130.00m, "EUR");
            Money priceBRL = new Money(750.00m, "BRL");
            UnavailableTimeSlots unavailableTimeSlots = CreateDefaultUnavailableTimeSlots();

            ProcedureType procedureTypeUSD = new ProcedureType(name, description, priceUSD, unavailableTimeSlots);
            ProcedureType procedureTypeEUR = new ProcedureType(name, description, priceEUR, unavailableTimeSlots);
            ProcedureType procedureTypeBRL = new ProcedureType(name, description, priceBRL, unavailableTimeSlots);

            procedureTypeUSD.Price.Currency.Should().Be("USD");
            procedureTypeEUR.Price.Currency.Should().Be("EUR");
            procedureTypeBRL.Price.Currency.Should().Be("BRL");
        }

        [Fact]
        public void MakeTimeUnavailable_ShouldSetUnavailableTimeSlots_WhenFutureDateIsProvided()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots initialSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, initialSlots);

            DateOnly futureDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            Date date = new Date(futureDate);
            UnavailableTimeSlots unavailableSlots = new UnavailableTimeSlots(date, new[] { new TimeOnly(10, 0) });

            procedureType.MakeTimeUnavailable(unavailableSlots);

            procedureType.UnavailableTimeSlots.Should().Be(unavailableSlots);
        }

        [Fact]
        public void MakeTimeUnavailable_ShouldThrow_WhenPastDateIsProvided()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots initialSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, initialSlots);

            DateOnly pastDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            Date date = new Date(pastDate);
            UnavailableTimeSlots unavailableSlots = new UnavailableTimeSlots(date, new[] { new TimeOnly(10, 0) });

            Action act = () => procedureType.MakeTimeUnavailable(unavailableSlots);

            act.Should().Throw<ArgumentException>().WithMessage("Cannot set unavailable times for dates in the past.");
        }

        [Fact]
        public void MakeTimeUnavailable_ShouldAllow_WhenTodayDateIsProvided()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots initialSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, initialSlots);

            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            Date date = new Date(todayDate);
            UnavailableTimeSlots unavailableSlots = new UnavailableTimeSlots(date, new[] { new TimeOnly(10, 0) });

            procedureType.MakeTimeUnavailable(unavailableSlots);

            procedureType.UnavailableTimeSlots.Should().Be(unavailableSlots);
        }

        [Fact]
        public void MakeTimeUnavailable_ShouldReturnEarly_WhenSameUnavailableTimeSlotsAreProvided()
        {
            string name = "Consultation";
            string description = "A medical consultation appointment.";
            Money price = new Money(150.00m, "USD");
            UnavailableTimeSlots unavailableSlots = CreateDefaultUnavailableTimeSlots();
            ProcedureType procedureType = new ProcedureType(name, description, price, unavailableSlots);

            var initialSlots = procedureType.UnavailableTimeSlots;

            procedureType.MakeTimeUnavailable(unavailableSlots);

            procedureType.UnavailableTimeSlots.Should().Be(initialSlots);
        }
    }
}
