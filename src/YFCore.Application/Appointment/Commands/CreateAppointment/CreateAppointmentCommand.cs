using System;
using System.Collections.Generic;

using MediatR;

namespace YFCore.Application.Appointment.Commands
{
    public sealed record CreateAppointmentCommand(
        decimal PriceAmount,
        string PriceCurrency,
        Guid ProcedureTypeId,
        Guid UserId,
        string Date,
        IEnumerable<string> TimeSlots
    ) : IRequest<Guid>;
}
