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
    public class AppointmentConfirmedHandler : INotificationHandler<DomainEventNotification<AppointmentConfirmed>>
    {
        private readonly INotificationService _notificationService;

        public AppointmentConfirmedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(DomainEventNotification<AppointmentConfirmed> notification, CancellationToken cancellationToken)
        {
            await _notificationService.SendAsync(notification.DomainEvent.Token, "Your appointment has been confirmed at " + notification.DomainEvent.ConfirmedAt + ".", "Appointment Confirmed");
        }
    }
}
