using Pizza4Ps.CustomerService.Domain.Abstractions.Services;
using Pizza4Ps.CustomerService.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Pizza4Ps.CustomerService.Domain.Services.ServiceBase;
using Pizza4Ps.CustomerService.Domain.Entities;
using Pizza4Ps.CustomerService.Domain.Abstractions.Repositories;

namespace Pizza4Ps.CustomerService.Domain.Services
{
    public class AuthService : DomainService, IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _customerRepository;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _customerRepository = customerRepository;
        }

        public async Task<bool> RegisterUserAsync(string phoneNumber, string password, string firstName, string lastName)
        {
            var existingUser = await _userManager.FindByNameAsync(phoneNumber);
            if (existingUser != null)
            {
                return false;
            }

            var user = new AppUser
            {
                PhoneNumber = phoneNumber,
                UserName = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                FullName = firstName + " " + lastName,
                IsDirector = false,
                IsHeadOfDepartment = false,
                IsReceipient = 1,
                PositionId = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = phoneNumber
                };

                _customerRepository.Add(customer);
                return true;
            }

            return result.Succeeded;
        }

        public async Task<string?> LoginUserAsync(string phoneNumber, string password)
        {
            var user = await _userManager.FindByNameAsync(phoneNumber);
            if (user == null)
            {
                return null;
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
            {
                return null;
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(AppUser user)
        {
            var secretKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Name, user.FullName.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.IsReceipient.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.PhoneNumber),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}