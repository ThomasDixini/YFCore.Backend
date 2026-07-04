using System;

using MediatR;

namespace YFCore.Application.Appointment.Commands
{
    public sealed record ScheduleAppointmentCommand(Guid Id) : IRequest<bool>;
}
