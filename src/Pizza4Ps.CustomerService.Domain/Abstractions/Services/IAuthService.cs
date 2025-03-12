using Pizza4Ps.CustomerService.Domain.Entities.Identity;

namespace Pizza4Ps.CustomerService.Domain.Abstractions.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(string phoneNumber, string password, string firstName, string lastName);
        Task<string> LoginUserAsync(string phoneNumber, string password);
    }
}