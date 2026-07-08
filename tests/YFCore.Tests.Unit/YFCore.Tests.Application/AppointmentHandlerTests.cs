using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YFCore.Application.Appointment.Commands;
using YFCore.Application.Appointment.Commands.CancelAppointment;
using YFCore.Application.Appointment.Commands.CreateAppointment;
using YFCore.Application.Appointment.Commands.FinishAppointment;
using YFCore.Application.Appointment.Commands.ScheduleAppointment;
using YFCore.Application.Appointment.DTOs;
using YFCore.Application.Appointment.Queries;
using YFCore.Application.Contracts;
using YFCore.Domain.AppointmentRepository;
using YFCore.Domain.Appointments.Entity;
using YFCore.Domain.Appointments.Enum;
using YFCore.Domain.Shared.Exceptions;
using YFCore.Domain.Shared.ValueObjects;
using YFCore.Domain.Users.Entity;
using YFCore.Domain.Users.Repository;

namespace YFCore.Tests.Unit.YFCore.Tests.Application
{
    public class AppointmentHandlerTests
    {
        [Fact]
        public async Task CreateAppointmentCommandHandler_ShouldAddAppointmentAndCommit()
        {
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            userRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User("Name", "LastName", new Phone("11999999999"), new Email("user@example.com"), "City"));
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new CreateAppointmentCommandHandler(appointmentRepository.Object, userRepository.Object, unitOfWork.Object);
            var request = new CreateAppointmentCommand(
                125m,
                "USD",
                Guid.NewGuid(),
                Guid.NewGuid(),
                "2026-06-09",
                new[] { "10:00" }
            );

            var result = await handler.Handle(request, CancellationToken.None);

            appointmentRepository.Verify(r => r.Add(It.IsAny<Appointment>()), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAppointmentCommandHandler_ShouldThrow_WhenUserDoesNotExist()
        {
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            userRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var handler = new CreateAppointmentCommandHandler(appointmentRepository.Object, userRepository.Object, unitOfWork.Object);
            var request = new CreateAppointmentCommand(
                125m,
                "USD",
                Guid.NewGuid(),
                Guid.NewGuid(),
                "2026-06-09",
                new[] { "10:00" }
            );

            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            await act.Should().ThrowAsync<DomainException>().WithMessage("User not found.");
            appointmentRepository.Verify(r => r.Add(It.IsAny<Appointment>()), Times.Never);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ScheduleAppointmentCommandHandler_ShouldUpdateAppointmentAndCommit()
        {
            var appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) }));
            var repository = new Mock<IAppointmentRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(appointment.Id)).ReturnsAsync(appointment);
            repository.Setup(r => r.GetAllWithStatusAsync()).ReturnsAsync(new List<Appointment>());
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new ScheduleAppointmentCommandHandler(repository.Object, unitOfWork.Object);
            var result = await handler.Handle(new ScheduleAppointmentCommand(appointment.Id), CancellationToken.None);

            result.Should().BeTrue();
            repository.Verify(r => r.Update(appointment), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
            appointment.Status.Should().Be(AppointmentStatus.Scheduled);
        }

        [Fact]
        public async Task ScheduleAppointmentCommandHandler_ShouldThrow_WhenAppointmentTimeIsAlreadyScheduled()
        {
            var scheduledAppointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) }));
            scheduledAppointment.Schedule();

            var appointmentToSchedule = new Appointment(Guid.NewGuid(), Guid.NewGuid(), new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) }));

            var repository = new Mock<IAppointmentRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(appointmentToSchedule.Id)).ReturnsAsync(appointmentToSchedule);
            repository.Setup(r => r.GetAllWithStatusAsync()).ReturnsAsync(new List<Appointment> { scheduledAppointment, appointmentToSchedule });

            var handler = new ScheduleAppointmentCommandHandler(repository.Object, unitOfWork.Object);

            Func<Task> act = async () => await handler.Handle(new ScheduleAppointmentCommand(appointmentToSchedule.Id), CancellationToken.None);

            await act.Should().ThrowAsync<DomainException>().WithMessage("Appointment time is already scheduled.");
            repository.Verify(r => r.Update(It.IsAny<Appointment>()), Times.Never);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ScheduleAppointmentCommandHandler_ShouldThrow_WhenAppointmentNotFound()
        {
            var repository = new Mock<IAppointmentRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Appointment?)null);

            var handler = new ScheduleAppointmentCommandHandler(repository.Object, unitOfWork.Object);

            Func<Task> act = async () => await handler.Handle(new ScheduleAppointmentCommand(Guid.NewGuid()), CancellationToken.None);

            await act.Should().ThrowAsync<DomainException>().WithMessage("Appointment not found.");
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task FinishAppointmentCommandHandler_ShouldCompleteAppointmentAndCommit()
        {
            var appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) }));
            appointment.Schedule();

            var repository = new Mock<IAppointmentRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(appointment.Id)).ReturnsAsync(appointment);
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new FinishAppointmentCommandHandler(repository.Object, unitOfWork.Object);
            var result = await handler.Handle(new FinishAppointmentCommand(appointment.Id), CancellationToken.None);

            result.Should().BeTrue();
            repository.Verify(r => r.Update(appointment), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
            appointment.Status.Should().Be(AppointmentStatus.Completed);
        }

        [Fact]
        public async Task CancelAppointmentCommandHandler_ShouldCancelAppointmentAndCommit()
        {
            var appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) }));
            var repository = new Mock<IAppointmentRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(appointment.Id)).ReturnsAsync(appointment);
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new CancelAppointmentCommandHandler(repository.Object, unitOfWork.Object);
            var result = await handler.Handle(new CancelAppointmentCommand(appointment.Id), CancellationToken.None);

            result.Should().BeTrue();
            repository.Verify(r => r.Update(appointment), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
            appointment.Status.Should().Be(AppointmentStatus.Cancelled);
        }

        [Fact]
        public async Task GetAppointmentByIdQueryHandler_ShouldReturnDto_WhenFound()
        {
            var appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) }));
            var repository = new Mock<IAppointmentRepository>();
            repository.Setup(r => r.GetByIdAsync(appointment.Id)).ReturnsAsync(appointment);

            var handler = new GetAppointmentByIdQueryHandler(repository.Object);
            var result = await handler.Handle(new GetAppointmentByIdQuery(appointment.Id), CancellationToken.None);

            result.Should().NotBeNull();
            result!.Id.Should().Be(appointment.Id);
            result.PriceAmount.Should().Be(appointment.Price.Amount);
            result.PriceCurrency.Should().Be(appointment.Price.Currency);
            result.Status.Should().Be(appointment.Status.ToString());
        }

        [Fact]
        public async Task GetAppointmentByIdQueryHandler_ShouldReturnNull_WhenNotFound()
        {
            var repository = new Mock<IAppointmentRepository>();
            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Appointment?)null);

            var handler = new GetAppointmentByIdQueryHandler(repository.Object);
            var result = await handler.Handle(new GetAppointmentByIdQuery(Guid.NewGuid()), CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task ListAppointmentsQueryHandler_ShouldReturnDtos()
        {
            var appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), new UnavailableTimeSlots(Date.Create(2026, 6, 9), new[] { new TimeOnly(10, 0) }));
            var repository = new Mock<IAppointmentRepository>();
            repository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Appointment> { appointment });

            var handler = new ListAppointmentsQueryHandler(repository.Object);
            var result = await handler.Handle(new ListAppointmentsQuery(), CancellationToken.None);

            result.Should().ContainSingle();
            var dto = result.Single();
            dto.Id.Should().Be(appointment.Id);
        }
    }
}
