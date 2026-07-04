using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Appointment.Commands;
using YFCore.Application.Contracts;
using YFCore.Domain.AppointmentRepository;
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
            appointment.Schedule();
            _appointmentRepository.Update(appointment);
            return await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
