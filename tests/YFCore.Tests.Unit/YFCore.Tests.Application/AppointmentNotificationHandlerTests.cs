using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YFCore.Application.Appointments.Handler;
using YFCore.Application.Contracts;
using YFCore.Application.Shared.Events;
using YFCore.Domain.Appointments.Events;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Application
{
    public class AppointmentNotificationHandlerTests
    {
        [Fact]
        public async Task AppointmentConfirmedHandler_ShouldSendNotification()
        {
            var notificationService = new Mock<INotificationService>();
            notificationService.Setup(s => s.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var handler = new AppointmentConfirmedHandler(notificationService.Object);
            var notification = new DomainEventNotification<AppointmentConfirmed>(new AppointmentConfirmed("token", new Date(2026, 6, 9)));

            await handler.Handle(notification, CancellationToken.None);

            notificationService.Verify(s => s.SendAsync(
                "token",
                It.Is<string>(m => m.Contains("Your appointment has been confirmed")),
                "Appointment Confirmed"
            ), Times.Once);
        }

        [Fact]
        public async Task AppointmentCancelledHandler_ShouldSendNotification()
        {
            var notificationService = new Mock<INotificationService>();
            notificationService.Setup(s => s.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var handler = new AppointmentCancelledHandler(notificationService.Object);
            var notification = new DomainEventNotification<AppointmentCancelled>(new AppointmentCancelled("token", new Date(2026, 6, 9)));

            await handler.Handle(notification, CancellationToken.None);

            notificationService.Verify(s => s.SendAsync(
                "token",
                It.Is<string>(m => m.Contains("Your appointment has been cancelled")),
                "Appointment Cancelled"
            ), Times.Once);
        }

        [Fact]
        public async Task AppointmentFinishedHandler_ShouldSendNotification()
        {
            var notificationService = new Mock<INotificationService>();
            notificationService.Setup(s => s.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var handler = new AppointmentFinishedHandler(notificationService.Object);
            var notification = new DomainEventNotification<AppointmentFinished>(new AppointmentFinished("token", new Date(2026, 6, 9)));

            await handler.Handle(notification, CancellationToken.None);

            notificationService.Verify(s => s.SendAsync(
                "token",
                It.Is<string>(m => m.Contains("Your appointment has been finished")),
                "Appointment Finished"
            ), Times.Once);
        }
    }
}
