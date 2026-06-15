using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Domain.Appointments.Events
{
    public class AppointmentConfirmed : DomainEventBase
    {
        public string Token { get; private set; }
        public Date ConfirmedAt { get; private set; }

        public AppointmentConfirmed(string token, Date confirmedAt)
        {
            this.Token = token;
            this.ConfirmedAt = confirmedAt;
        }
    }
}