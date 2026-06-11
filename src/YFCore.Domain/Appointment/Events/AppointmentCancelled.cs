using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;

namespace YFCore.Domain.Appointment.Events
{
    public class AppointmentCancelled : DomainEventBase
    {
        public Guid AppointmentId { get; private set; }
        public DateTime CancelledAt { get; private set; }

        public AppointmentCancelled(Guid appointmentId, DateTime cancelledAt)
        {
            this.AppointmentId = appointmentId;
            this.CancelledAt = cancelledAt;
        }
    }
}