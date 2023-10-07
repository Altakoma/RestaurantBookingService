using IdentityService.DataAccess.Entities;

namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserAsync(string login, string password);
    }
}
