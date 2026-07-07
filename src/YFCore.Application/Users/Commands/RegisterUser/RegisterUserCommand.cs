using MediatR;

using YFCore.Application.Users.DTOs;

namespace YFCore.Application.Users.Commands.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Name,
        string LastName,
        string Phone,
        string Email,
        string City,
        string Password
    ) : IRequest<AuthResultDto>;
}
