using System.Threading.Tasks;

using YFCore.Domain.Shared.Repository;
using YFCore.Domain.Users.Entity;

namespace YFCore.Domain.Users.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
