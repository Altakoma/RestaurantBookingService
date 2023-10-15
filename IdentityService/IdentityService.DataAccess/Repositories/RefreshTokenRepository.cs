using AutoMapper;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.DTOs.RefreshToken;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Base;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class RefreshTokenRepository :
        WriteRepository<RefreshToken>,
        IRefreshTokenRepository
    {
        private readonly IdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;

        public RefreshTokenRepository(IdentityDbContext identityDbContext,
            IMapper mapper) : base(identityDbContext)
        {
            _identityDbContext = identityDbContext;
            _mapper = mapper;
        }

        public async Task<CreationRefreshTokenDTO?> GetCreationRefreshTokenDTOAsync(string token,
            CancellationToken cancellationToken)
        {
            var creationRefreshTokenDTO = await _mapper.ProjectTo<CreationRefreshTokenDTO>(
                _identityDbContext.RefreshTokens
                .AsNoTracking()
                .Where(refreshToken => refreshToken.Token == token))
                .SingleOrDefaultAsync(cancellationToken);

            return creationRefreshTokenDTO;
        }

        public async Task<RefreshToken?> GetByUserIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var refreshToken = await _identityDbContext.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    refreshToken => refreshToken.UserId == id,
                    cancellationToken);

            return refreshToken;
        }
    }
}
