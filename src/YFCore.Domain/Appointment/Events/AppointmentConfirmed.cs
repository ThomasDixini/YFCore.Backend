using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;

namespace YFCore.Domain.Appointment.Events
{
    public class AppointmentConfirmed : DomainEventBase
    {
        public Guid AppointmentId { get; private set; }
        public DateTime ConfirmedAt { get; private set; }

        public AppointmentConfirmed(Guid appointmentId, DateTime confirmedAt)
        {
            this.AppointmentId = appointmentId;
            this.ConfirmedAt = confirmedAt;
        }
    }
}