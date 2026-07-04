using System;

using MediatR;

namespace YFCore.Application.Appointment.Commands
{
    public sealed record FinishAppointmentCommand(Guid Id) : IRequest<bool>;
}
