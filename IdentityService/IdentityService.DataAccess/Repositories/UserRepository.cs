using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public UserRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<bool> DeleteAsync(User item)
        {
            _identityDbContext.Remove(item);
            return await _identityDbContext.SaveChangesToDbAsync();
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            var users = await _identityDbContext.Users.Include(u => u.UserRole)
                .Select(u => u).ToListAsync();

            return users;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _identityDbContext.Users.Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User?> GetUserAsync(string login, string password)
        {
            var user = await _identityDbContext.Users.Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Login == login &&
                u.Password == password);

            return user;
        }

        public async Task<(User, bool)> InsertAsync(User item)
        {
            await _identityDbContext.AddAsync(item);
            bool isInserted = await _identityDbContext.SaveChangesToDbAsync();

            return (item, isInserted);
        }

        public async Task<bool> UpdateAsync(User item)
        {
            _identityDbContext.Update(item);
            return await _identityDbContext.SaveChangesToDbAsync();
        }
    }
}
