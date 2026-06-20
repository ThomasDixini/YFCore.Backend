using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Application.Contracts;
using YFCore.Domain.Appointments.Events;

namespace YFCore.Application.Appointment.Handler
{
    public class AppointmentConfirmedHandler
    {
        private readonly INotificationService _notificationService;

        public AppointmentConfirmedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(AppointmentConfirmed appointmentConfirmed)
        {
            await _notificationService.SendAsync(appointmentConfirmed.Token, "Your appointment has been confirmed at " + appointmentConfirmed.ConfirmedAt + ".", "Appointment Confirmed");
        }
    }
}
