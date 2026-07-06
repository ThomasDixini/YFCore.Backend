using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Application.Shared.Events;
using YFCore.Domain.Appointments.Events;

namespace YFCore.Application.Appointments.Handler
{
    public class AppointmentFinishedHandler : INotificationHandler<DomainEventNotification<AppointmentFinished>>
    {
        private readonly INotificationService _notificationService;

        public AppointmentFinishedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(DomainEventNotification<AppointmentFinished> notification, CancellationToken cancellationToken)
        {
            await _notificationService.SendAsync(notification.DomainEvent.Token, "Your appointment has been finished at " + notification.DomainEvent.FinishedAt + ".", "Appointment Finished");
        }
    }
}
