using MediatR;

using YFCore.Application.Users.DTOs;

namespace YFCore.Application.Users.Commands.LoginUser
{
    public sealed record LoginUserCommand(string Email, string Password) : IRequest<AuthResultDto>;
}
