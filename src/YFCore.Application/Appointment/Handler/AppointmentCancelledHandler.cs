using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Application.Contracts;
using YFCore.Domain.Appointments.Events;

namespace YFCore.Application.Appointments.Handler
{
    public class AppointmentCancelledHandler
    {
        private readonly INotificationService _notificationService;

        public AppointmentCancelledHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(AppointmentCancelled appointmentCancelled)
        {
            await _notificationService.SendAsync(appointmentCancelled.Token, "Your appointment has been cancelled at " + appointmentCancelled.CancelledAt + ".", "Appointment Cancelled");
        }
    }
}
