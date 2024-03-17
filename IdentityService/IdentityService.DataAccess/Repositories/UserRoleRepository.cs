using AutoMapper;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Base;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>,
        IUserRoleRepository
    {
        public UserRoleRepository(IdentityDbContext identityDbContext,
            IMapper mapper) : base(identityDbContext, mapper)
        {
        }
    }
}
