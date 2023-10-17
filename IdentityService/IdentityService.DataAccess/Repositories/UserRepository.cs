using AutoMapper;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Base;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IdentityDbContext identityDbContext,
            IMapper mapper) : base(identityDbContext, mapper)
        {
        }

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? user = await _mapper.ProjectTo<U>(
                _identityDbContext.Users
                .Where(user => user.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return user;
        }

        public async Task<U?> GetUserAsync<U>(string login, string password,
            CancellationToken cancellationToken)
        {
            U? user = await _mapper.ProjectTo<U>(
                _identityDbContext.Users
                .Where(user => user.Login == login &&
                       user.Password == password))
                .SingleOrDefaultAsync(cancellationToken);

            return user;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            User? user = await _identityDbContext.Users
                .FirstOrDefaultAsync(user => user.Id == id,
                cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(id.ToString(), typeof(User));
            }

            base.Delete(user);
        }
    }
}
