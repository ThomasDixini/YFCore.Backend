using System;

using MediatR;

using YFCore.Application.Appointment.DTOs;

namespace YFCore.Application.Appointment.Queries
{
    public sealed record GetAppointmentByIdQuery(Guid Id) : IRequest<AppointmentDTO?>;
}
