using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using YFCore.Domain.Shared.Repository;
using YFCore.Domain.Users.Entity;
using YFCore.Domain.Users.Repository;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Shared;

namespace YFCore.Infraestructure.Repository.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var normalizedEmail = email?.Trim().ToLowerInvariant();
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email.Value == normalizedEmail);
        }
    }
}
