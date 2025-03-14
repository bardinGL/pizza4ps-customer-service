using MediatR;
using Pizza4Ps.CustomerService.Application.Abstractions;

namespace Pizza4Ps.CustomerService.Application.UserCases.V1.Auth.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<ResultDto<bool>>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

    }
}
