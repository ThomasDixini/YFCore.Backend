using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Domain.Appointments.Events
{
    public class AppointmentCancelled : DomainEventBase
    {
        public string Token { get; private set; }
        public Date CancelledAt { get; private set; }

        public AppointmentCancelled(string token, Date cancelledAt)
        {
            this.Token = token;
            this.CancelledAt = cancelledAt;
        }
    }
}
