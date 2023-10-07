using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces;
using IdentityServiceDataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public UserRoleRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<ICollection<UserRole>?> GetAllAsync()
        {
            var roles = await _identityDbContext.UserRoles
                .Select(ur => ur).ToListAsync();

            return roles;
        }

        public async Task<UserRole?> GetByIdAsync(int id)
        {
            var role = await _identityDbContext.UserRoles.
                FirstOrDefaultAsync(ur => ur.Id == id);

            return role;
        }

        public async Task<bool> InsertAsync(UserRole item)
        {
            await _identityDbContext.AddAsync(item);
            return await _identityDbContext.SaveChangesToDbAsync();
        }
    }
}
