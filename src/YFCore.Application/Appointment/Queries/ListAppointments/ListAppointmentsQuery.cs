using System.Collections.Generic;

using MediatR;

using YFCore.Application.Appointment.DTOs;

namespace YFCore.Application.Appointment.Queries
{
    public sealed class ListAppointmentsQuery : IRequest<IEnumerable<AppointmentDTO>>
    {
    }
}
