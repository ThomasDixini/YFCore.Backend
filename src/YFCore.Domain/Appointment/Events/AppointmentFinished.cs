using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;

namespace YFCore.Domain.Appointment.Events
{
    public class AppointmentFinished : DomainEventBase
    {
        public Guid AppointmentId { get; private set; }
        public DateTime FinishedAt { get; private set; }

        public AppointmentFinished(Guid appointmentId, DateTime finishedAt)
        {
            this.AppointmentId = appointmentId;
            this.FinishedAt = finishedAt;
        }
    }
}