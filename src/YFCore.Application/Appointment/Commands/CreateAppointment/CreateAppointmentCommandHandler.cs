using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Appointment.Commands;
using YFCore.Application.Contracts;
using YFCore.Domain.AppointmentRepository;
using YFCore.Domain.Shared.ValueObjects;
using YFCore.Domain.Shared.Exceptions;
using AppointmentEntity = YFCore.Domain.Appointments.Entity.Appointment;

namespace YFCore.Application.Appointment.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var date = DateOnly.Parse(request.Date);
            var timeSlots = request.TimeSlots.Select(TimeOnly.Parse);
            var appointment = new AppointmentEntity(
                request.ProcedureTypeId,
                request.UserId,
                new UnavailableTimeSlots(new Date(date), timeSlots)
            );
            appointment.ChangePrice(new Money(request.PriceAmount, request.PriceCurrency));

            _appointmentRepository.Add(appointment);
            await _unitOfWork.CommitAsync(cancellationToken);
            return appointment.Id;
        }
    }
}
