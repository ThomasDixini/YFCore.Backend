using YFCore.Domain.Appointments.Entity;
using YFCore.Domain.Shared.Repository;

namespace YFCore.Domain.AppointmentRepository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<ICollection<Appointment>> GetAllWithStatusAsync();
        Task<Appointment?> GetByIdAsync(Guid id);
    }
}
