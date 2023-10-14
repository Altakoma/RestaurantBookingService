using IdentityService.DataAccess.DTOs.User;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;

namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ReadUserDTO, User>
    {
        Task<ReadUserDTO?> GetUserAsync(string login, string password, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
