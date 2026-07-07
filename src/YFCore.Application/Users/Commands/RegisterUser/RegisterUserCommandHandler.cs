using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Identity;

using YFCore.Application.Contracts;
using YFCore.Application.Users.DTOs;
using YFCore.Domain.Shared.ValueObjects;
using YFCore.Domain.Users.Entity;
using YFCore.Domain.Users.Repository;
using YFCore.Application.Users.Commands.RegisterUser;

namespace YFCore.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResultDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher<User> passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim();
            var existingUser = await _userRepository.GetByEmailAsync(normalizedEmail);
            if (existingUser is not null)
            {
                throw new InvalidOperationException("A user with the provided email already exists.");
            }

            var user = new User(
                request.Name,
                request.LastName,
                new Phone(request.Phone),
                new Email(normalizedEmail),
                request.City
            );

            user.SetPasswordHash(_passwordHasher.HashPassword(user, request.Password));

            _userRepository.Add(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthResultDto(user.Id, token);
        }
    }
}
