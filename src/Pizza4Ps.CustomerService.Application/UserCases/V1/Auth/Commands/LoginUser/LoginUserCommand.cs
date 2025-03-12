using MediatR;
using Pizza4Ps.CustomerService.Application.Abstractions;

namespace Pizza4Ps.CustomerService.Application.UserCases.V1.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<ResultDto<string>>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
