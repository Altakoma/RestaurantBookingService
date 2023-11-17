using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.CacheAccess.Interfaces;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityService.BusinessLogic.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public const int ExpirationTime = 25;

        private readonly IRefreshTokenCacheAccessor _refreshTokenAccessor;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ICookieService _cookieService;
        private readonly IUserRepository _userRepository;

        public RefreshTokenService(IRefreshTokenCacheAccessor refreshTokenAccessor,
            ITokenGenerator tokenGenerator,
            ICookieService cookieService,
            IUserRepository userRepository)
        {
            _refreshTokenAccessor = refreshTokenAccessor;
            _tokenGenerator = tokenGenerator;
            _cookieService = cookieService;
            _userRepository = userRepository;
        }

        public async Task<AccessTokenDTO> VerifyAndGenerateTokenAsync(
            CancellationToken cancellationToken)
        {
            string userId = _cookieService.GetCookieValue(CookieService.UserIdCookieName);

            string currentRefreshToken = await _refreshTokenAccessor
                                               .GetByUserIdAsync(userId, cancellationToken);

            string userRefreshToken = _cookieService
                                      .GetCookieValue(CookieService.RefreshTokenCookieName);

            if (currentRefreshToken != userRefreshToken)
            {
                throw new NotFoundException(nameof(userRefreshToken), typeof(User));
            }

            User user = (await _userRepository
                              .GetByIdAsync<User>(int.Parse(userId), cancellationToken))!;

            (AccessTokenDTO tokenDTO, string refreshToken) =
                _tokenGenerator.GenerateTokens(user.Name, user.UserRole.Name, userId);

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

            await _refreshTokenAccessor.SetAsync(userId, refreshToken,
                options, cancellationToken);
        }

        public async Task DeleteByIdAsync(string userId, CancellationToken cancellationToken)
        {
            await _refreshTokenAccessor.DeleteByIdAsync(userId, cancellationToken);
        }
    }
}
