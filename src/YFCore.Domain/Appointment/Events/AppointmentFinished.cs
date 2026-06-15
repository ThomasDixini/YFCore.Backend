using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Domain.Appointments.Events
{
    public class AppointmentFinished : DomainEventBase
    {
        public string Token { get; private set; }
        public Date FinishedAt { get; private set; }

        public AppointmentFinished(string token, Date finishedAt)
        {
            this.Token = token;
            this.FinishedAt = finishedAt;
        }
    }
}