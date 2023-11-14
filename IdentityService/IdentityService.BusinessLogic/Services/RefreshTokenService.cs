using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.CacheAccess.Interfaces;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityService.BusinessLogic.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public const int ExpirationTime = 25;

        private readonly IRefreshTokenCacheAccessor _refreshTokenRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ICookieService _cookieService;

        public RefreshTokenService(IRefreshTokenCacheAccessor refreshTokenRepository,
            ITokenGenerator tokenGenerator,
            ICookieService cookieService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenGenerator = tokenGenerator;
            _cookieService = cookieService;
        }

        public async Task<AccessTokenDTO> VerifyAndGenerateTokenAsync(
            CancellationToken cancellationToken)
        {
            string userId = _cookieService.GetCookieValue(CookieService.UserIdCookieName);

            string currentRefreshToken = await _refreshTokenRepository
                                               .GetByUserIdAsync(userId, cancellationToken);

            string userRefreshToken = _cookieService
                                      .GetCookieValue(CookieService.RefreshTokenCookieName);

            if (currentRefreshToken != userRefreshToken)
            {
                throw new NotFoundException(nameof(userRefreshToken), typeof(User));
            }

            string userName = _cookieService.GetCookieValue(CookieService.UserNameCookieName);
            string userRoleName = _cookieService.GetCookieValue(CookieService.UserRoleNameCookieName);

            (AccessTokenDTO tokenDTO, string refreshToken) =
                _tokenGenerator.GenerateTokens(userName, userRoleName, userId);

            await SetAsync(userId, refreshToken, ExpirationTime, cancellationToken);

            _cookieService.SetCookieValue(CookieService.RefreshTokenCookieName, refreshToken);

            return tokenDTO;
        }

        public async Task SetAsync(string userId, string refreshToken, int time,
            CancellationToken cancellationToken)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(time),
            };

            await _refreshTokenRepository.SetAsync(userId, refreshToken,
                options, cancellationToken);
        }
    }
}
