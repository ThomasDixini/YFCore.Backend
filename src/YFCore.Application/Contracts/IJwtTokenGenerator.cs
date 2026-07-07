using YFCore.Domain.Users.Entity;

namespace YFCore.Application.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
