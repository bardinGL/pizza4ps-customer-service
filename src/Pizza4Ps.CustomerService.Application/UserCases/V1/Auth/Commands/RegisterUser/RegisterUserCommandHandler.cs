using MediatR;
using Pizza4Ps.CustomerService.Application.Abstractions;
using Pizza4Ps.CustomerService.Domain.Abstractions.Services;

namespace Pizza4Ps.CustomerService.Application.UserCases.V1.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDto<bool>>
    {
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResultDto<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var success = await _authService.RegisterUserAsync(request.PhoneNumber, request.Password, request.FirstName, request.LastName);
            return new ResultDto<bool> { Id = success };
        }
    }
}
