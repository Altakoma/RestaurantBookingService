using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces;
using IdentityService.DataAccess.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class UserRoleRepository : WriteRepository<UserRole>,
        IUserRoleRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public UserRoleRepository(IdentityDbContext identityDbContext)
            : base(identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<ICollection<UserRole>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var roles = await _identityDbContext.UserRoles
                .AsNoTracking()
                .Select(userRole => userRole).ToListAsync(cancellationToken);

            return roles;
        }

        public async Task<UserRole?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var role = await _identityDbContext.UserRoles
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    userRole => userRole.Id == id, cancellationToken);

            return role;
        }
    }
}
