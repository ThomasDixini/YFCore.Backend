using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using YFCore.Domain.AppointmentRepository;
using YFCore.Domain.Appointments.Entity;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Shared;

namespace YFCore.Infraestructure.Repository.Appointments
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context) { }

        public async Task<ICollection<Appointment>> GetAllWithStatusAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public new async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments.FindAsync(id);
        }
    }
}
