using AutoMapper;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.DTOs.User;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Base;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class UserRepository : WriteRepository<User>, IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;

        public UserRepository(IdentityDbContext identityDbContext,
            IMapper mapper) : base(identityDbContext)
        {
            _identityDbContext = identityDbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<ReadUserDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var users = await _mapper.ProjectTo<ReadUserDTO>(
                _identityDbContext.Users
                .AsNoTracking()
                .Select(user => user))
                .ToListAsync(cancellationToken);

            return users;
        }

        public async Task<ReadUserDTO?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var user = await _mapper.ProjectTo<ReadUserDTO>(
                _identityDbContext.Users
                .AsNoTracking()
                .Where(user => user.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return user;
        }

        public async Task<ReadUserDTO?> GetUserAsync(string login, string password,
            CancellationToken cancellationToken)
        {
            var user = await _mapper.ProjectTo<ReadUserDTO>(
                _identityDbContext.Users
                .AsNoTracking()
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
