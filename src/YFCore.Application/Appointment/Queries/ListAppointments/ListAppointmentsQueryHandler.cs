using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Appointment.DTOs;
using YFCore.Domain.AppointmentRepository;

namespace YFCore.Application.Appointment.Queries
{
    public class ListAppointmentsQueryHandler : IRequestHandler<ListAppointmentsQuery, IEnumerable<AppointmentDTO>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ListAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentDTO>> Handle(ListAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Select(appointment => new AppointmentDTO(
                appointment.Id,
                appointment.Price.Amount,
                appointment.Price.Currency,
                appointment.ProcedureTypeId,
                appointment.UserId,
                appointment.Token,
                appointment.Status.ToString(),
                appointment.TimeSlots.ToString()
            ));
        }
    }
}
