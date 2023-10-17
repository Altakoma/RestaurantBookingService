using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;

namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
