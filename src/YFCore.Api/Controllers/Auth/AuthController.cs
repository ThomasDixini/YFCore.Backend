using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using YFCore.Api.Controllers;
using YFCore.Application.Users.Commands.LoginUser;
using YFCore.Application.Users.Commands.RegisterUser;
using YFCore.Application.Users.DTOs;

namespace YFCore.Api.Controllers.Auth
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResultDto>>> Register(RegisterUserCommand command)
        {
            var authResult = await _mediator.Send(command);
            return CreatedResponse(nameof(Register), null, authResult, "User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResultDto>>> Login(LoginUserCommand command)
        {
            var authResult = await _mediator.Send(command);
            return OkResponse(authResult, "User logged in successfully.");
        }
    }
}
