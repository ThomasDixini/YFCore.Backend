using System;

using MediatR;

namespace YFCore.Application.Appointment.Commands
{
    public sealed record CancelAppointmentCommand(Guid Id) : IRequest<bool>;
}
