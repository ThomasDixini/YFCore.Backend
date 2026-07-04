using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Appointment.DTOs;
using YFCore.Domain.AppointmentRepository;

namespace YFCore.Application.Appointment.Queries
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDTO?>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentDTO?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment is null)
                return null;

            return new AppointmentDTO(
                appointment.Id,
                appointment.Price.Amount,
                appointment.Price.Currency,
                appointment.ProcedureTypeId,
                appointment.UserId,
                appointment.Token,
                appointment.Status.ToString(),
                appointment.TimeSlots.ToString()
            );
        }
    }
}
