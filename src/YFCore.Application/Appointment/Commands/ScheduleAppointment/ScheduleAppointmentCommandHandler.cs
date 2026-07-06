using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Appointment.Commands;
using YFCore.Application.Contracts;
using YFCore.Domain.AppointmentRepository;
using YFCore.Domain.Appointments.Entity;
using YFCore.Domain.Appointments.Enum;
using YFCore.Domain.Shared.Exceptions;

namespace YFCore.Application.Appointment.Commands.ScheduleAppointment
{
    public class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ScheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id) ?? throw new DomainException("Appointment not found.");

            var allAppointments = await _appointmentRepository.GetAllWithStatusAsync();
            var appointmentTimeSlots = appointment.TimeSlots.TimeSlots;
            var hasConflict = allAppointments != null && allAppointments
                .Where(a => a.Status == AppointmentStatus.Scheduled && a.TimeSlots.Date.Value == appointment.TimeSlots.Date.Value)
                .Any(a => a.TimeSlots.TimeSlots.Intersect(appointmentTimeSlots).Any());

            if (hasConflict)
                throw new DomainException("Appointment time is already scheduled.");

            appointment.Schedule();
            _appointmentRepository.Update(appointment);
            return await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
