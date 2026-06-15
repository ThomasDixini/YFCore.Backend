using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Application.Contracts;
using YFCore.Domain.Appointments.Events;

namespace YFCore.Application.Appointment.Handler
{
    public class AppointmentFinishedHandler
    {
        private readonly INotificationService _notificationService;

        public AppointmentFinishedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(AppointmentFinished appointmentFinished)
        {
            await _notificationService.SendAsync(appointmentFinished.Token, "Your appointment has been finished at " + appointmentFinished.FinishedAt + ".", "Appointment Finished");
        }
    }
}