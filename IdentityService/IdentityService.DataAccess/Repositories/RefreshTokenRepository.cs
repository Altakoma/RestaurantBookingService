using AutoMapper;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Base;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>,
        IRefreshTokenRepository
    {
        public RefreshTokenRepository(IdentityDbContext identityDbContext,
            IMapper mapper) : base(identityDbContext, mapper)
        {
        }

        public async Task<U?> GetCreationRefreshTokenDTOAsync<U>(string token,
            CancellationToken cancellationToken)
        {
            U? creationRefreshTokenDTO = await _mapper.ProjectTo<U>(
                _identityDbContext.RefreshTokens
                .Where(refreshToken => refreshToken.Token == token))
                .SingleOrDefaultAsync(cancellationToken);

            return creationRefreshTokenDTO;
        }

        public async Task<RefreshToken?> GetByUserIdAsync(int id,
            CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _identityDbContext.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    refreshToken => refreshToken.Id == id,
                    cancellationToken);

            return refreshToken;
        }
    }
}
