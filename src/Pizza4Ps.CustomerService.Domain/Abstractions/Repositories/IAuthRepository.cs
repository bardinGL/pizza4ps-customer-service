using Pizza4Ps.CustomerService.Domain.Abstractions.Repositories.RepositoryBase;
using Pizza4Ps.CustomerService.Domain.Entities.Identity;

namespace Pizza4Ps.CustomerService.Domain.Abstractions.Repositories
{
    public interface IAuthRepository : IRepositoryBase<AppUser, Guid>
    {

    }
}