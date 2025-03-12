using MediatR;
using Pizza4Ps.CustomerService.Application.Abstractions;
using Pizza4Ps.CustomerService.Domain.Abstractions.Services;

namespace Pizza4Ps.CustomerService.Application.UserCases.V1.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDto<string>>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResultDto<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginUserAsync(request.PhoneNumber, request.Password);

            return new ResultDto<string> 
            { 
                Id = token
            };
        }
    }
}
