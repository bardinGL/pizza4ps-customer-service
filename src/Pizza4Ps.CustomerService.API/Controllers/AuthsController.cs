using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pizza4Ps.CustomerService.API.Constants;
using Pizza4Ps.CustomerService.API.Models;
using Pizza4Ps.CustomerService.Application.UserCases.V1.Auth.Commands.LoginUser;
using Pizza4Ps.CustomerService.Application.UserCases.V1.Auth.Commands.RegisterUser;

namespace Pizza4Ps.CustomerService.API.Controllers
{
    [Route("api/auths")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISender _sender;

        public AuthsController(IHttpContextAccessor httpContextAccessor, ISender sender)
        {
            _httpContextAccessor = httpContextAccessor;
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(new ApiResponse
            {
                Result = result,
                Message = Message.CREATED_SUCCESS,
                StatusCode = StatusCodes.Status201Created
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(new ApiResponse
            {
                Result = result,
                Message = Message.GET_SUCCESS,
                StatusCode = StatusCodes.Status200OK
            });
        }
    }
}