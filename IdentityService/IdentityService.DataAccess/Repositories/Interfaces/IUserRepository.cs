using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;

namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<U?> GetUserAsync<U>(string login, string password, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
