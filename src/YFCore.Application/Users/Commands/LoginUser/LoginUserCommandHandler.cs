using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Identity;

using YFCore.Application.Contracts;
using YFCore.Application.Users.DTOs;
using YFCore.Domain.Users.Repository;
using YFCore.Domain.Users.Entity;
using YFCore.Application.Users.Commands.LoginUser;

namespace YFCore.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResultDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim();
            var user = await _userRepository.GetByEmailAsync(normalizedEmail);
            if (user is null)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthResultDto(user.Id, token);
        }
    }
}
