using System;

using FluentAssertions;
using Xunit;

using YFCore.Domain.Appointments.Entity;
using YFCore.Domain.Appointments.Enum;
using YFCore.Domain.Appointments.Events;
using YFCore.Domain.Shared.Exceptions;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsAppointment
{
    public class AppointmentTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidDataIsProvided()
        {
            var procedureTypeId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var timeSlots = new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) });

            var appointment = new Appointment(procedureTypeId, userId, timeSlots);

            appointment.Status.Should().Be(AppointmentStatus.AwaitingConfirmation);
            appointment.Price.Amount.Should().Be(0m);
            appointment.Price.Currency.Should().Be("USD");
            appointment.ProcedureTypeId.Should().Be(procedureTypeId);
            appointment.UserId.Should().Be(userId);
            appointment.Token.Should().NotBeNullOrWhiteSpace();
            appointment.TimeSlots.Should().Be(timeSlots);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenProcedureTypeIdIsEmpty()
        {
            var userId = Guid.NewGuid();
            var timeSlots = new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) });

            Action act = () => new Appointment(Guid.Empty, userId, timeSlots);

            act.Should().Throw<ArgumentException>().WithMessage("ProcedureTypeId cannot be empty.");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenUserIdIsEmpty()
        {
            var procedureTypeId = Guid.NewGuid();
            var timeSlots = new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) });

            Action act = () => new Appointment(procedureTypeId, Guid.Empty, timeSlots);

            act.Should().Throw<ArgumentException>().WithMessage("UserId cannot be empty.");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenTimeSlotsIsNull()
        {
            var procedureTypeId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            Action act = () => new Appointment(procedureTypeId, userId, null!);

            act.Should().Throw<ArgumentException>().WithMessage("TimeSlots cannot be null or empty.");
        }

        [Fact]
        public void ChangePrice_ShouldUpdatePrice_WhenValid()
        {
            var appointment = CreateDefaultAppointment();
            var newPrice = new Money(200m, "USD");

            appointment.ChangePrice(newPrice);

            appointment.Price.Should().Be(newPrice);
        }

        [Fact]
        public void ChangePrice_ShouldThrow_WhenCurrencyIsNull()
        {
            var appointment = CreateDefaultAppointment();
            var invalidPrice = new Money(200m, null);

            Action act = () => appointment.ChangePrice(invalidPrice);

            act.Should().Throw<ArgumentNullException>().WithMessage("*Currency*");
        }

        [Fact]
        public void ChangePrice_ShouldThrow_WhenPriceIsNegative()
        {
            var appointment = CreateDefaultAppointment();
            var invalidPrice = new Money(-10m, "USD");

            Action act = () => appointment.ChangePrice(invalidPrice);

            act.Should().Throw<ArgumentException>().WithMessage("Price cannot be negative.");
        }

        [Fact]
        public void Schedule_ShouldSetStatusToScheduled_AndAddAppointmentConfirmedEvent()
        {
            var appointment = CreateDefaultAppointment();

            appointment.Schedule();

            appointment.Status.Should().Be(AppointmentStatus.Scheduled);
            appointment.DomainEvents.Should().ContainSingle(e => e is AppointmentConfirmed && ((AppointmentConfirmed)e).Token == appointment.Token);
        }

        [Fact]
        public void Schedule_ShouldThrow_WhenAlreadyScheduled()
        {
            var appointment = CreateDefaultAppointment();
            appointment.Schedule();

            Action act = () => appointment.Schedule();

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Cancel_ShouldSetStatusToCancelled_AndAddAppointmentCancelledEvent()
        {
            var appointment = CreateDefaultAppointment();

            appointment.Cancel();

            appointment.Status.Should().Be(AppointmentStatus.Cancelled);
            appointment.DomainEvents.Should().ContainSingle(e => e is AppointmentCancelled && ((AppointmentCancelled)e).Token == appointment.Token);
        }

        [Fact]
        public void Cancel_ShouldThrow_WhenAlreadyCancelled()
        {
            var appointment = CreateDefaultAppointment();
            appointment.Cancel();

            Action act = () => appointment.Cancel();

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Cancel_ShouldThrow_WhenCompleted()
        {
            var appointment = CreateDefaultAppointment();
            appointment.Schedule();
            appointment.Complete();

            Action act = () => appointment.Cancel();

            act.Should().Throw<DomainException>().WithMessage("A completed appointment cannot be cancelled.");
        }

        [Fact]
        public void Complete_ShouldSetStatusToCompleted_AndAddAppointmentFinishedEvent()
        {
            var appointment = CreateDefaultAppointment();
            appointment.Schedule();

            appointment.Complete();

            appointment.Status.Should().Be(AppointmentStatus.Completed);
            appointment.DomainEvents.Should().ContainSingle(e => e is AppointmentFinished && ((AppointmentFinished)e).Token == appointment.Token);
        }

        [Fact]
        public void Complete_ShouldThrow_WhenNotScheduled()
        {
            var appointment = CreateDefaultAppointment();

            Action act = () => appointment.Complete();

            act.Should().Throw<DomainException>().WithMessage("Only scheduled appointments can be completed.");
        }

        [Fact]
        public void ClearDomainEvents_ShouldRemoveAllRecordedEvents()
        {
            var appointment = CreateDefaultAppointment();
            appointment.Schedule();
            appointment.ClearDomainEvents();

            appointment.DomainEvents.Should().BeEmpty();
        }

        private static Appointment CreateDefaultAppointment()
        {
            var procedureTypeId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var timeSlots = new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) });

            return new Appointment(procedureTypeId, userId, timeSlots);
        }
    }
}
