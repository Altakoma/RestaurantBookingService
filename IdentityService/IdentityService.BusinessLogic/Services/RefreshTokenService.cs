using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.DTOs.RefreshToken;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace IdentityService.BusinessLogic.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public const string RefreshTokenCookieName = "RefreshToken";

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public string RefreshTokenCookie
        {
            get
            {
                var refreshTokenString = _httpContextAccessor.HttpContext.Request
                .Cookies.FirstOrDefault(c => c.Key == RefreshTokenCookieName).Value;

                return refreshTokenString;
            }
            set
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddMinutes(20),
                };

                _httpContextAccessor.HttpContext.Response
                    .Cookies.Append(RefreshTokenCookieName, value, cookieOptions);
            }
        }

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            TokenValidationParameters tokenValidationParams,
            ITokenGenerator tokenGenerator,
            IHttpContextAccessor httpContextAccessor)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenValidationParameters = tokenValidationParams;
            _tokenGenerator = tokenGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository
                                     .GetByUserIdAsync(id, cancellationToken);

            if (refreshToken is null)
            {
                throw new NotFoundException(id.ToString(), typeof(RefreshToken));
            }

            _refreshTokenRepository.Delete(refreshToken);

            bool isDeleted = await _refreshTokenRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(
                    nameof(DeleteAsync), id.ToString(), typeof(RefreshToken));
            }
        }

        public async Task<RefreshToken?> GetByUserIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository
                              .GetByUserIdAsync(id, cancellationToken);

            return token;
        }

        public async Task SaveTokenAsync(RefreshToken token,
            CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await GetByUserIdAsync(
                                         token.UserId, cancellationToken);

            if (refreshToken is null)
            {
                await _refreshTokenRepository
                      .InsertAsync(token, cancellationToken);
            }
            else
            {
                _refreshTokenRepository.Update(token);
            }
        }

        public async Task<TokenDTO> VerifyAndGenerateTokenAsync(
            CancellationToken cancellationToken)
        {
            string refreshTokenString = RefreshTokenCookie;

            if (refreshTokenString is null)
            {
                throw new NotFoundException(RefreshTokenCookieName, typeof(Cookie));
            }

            CreationRefreshTokenDTO? creationRefreshTokenDTO = await _refreshTokenRepository
                .GetCreationRefreshTokenDTOAsync(refreshTokenString, cancellationToken);

            if (creationRefreshTokenDTO is null)
            {
                throw new NotFoundException(nameof(refreshTokenString), typeof(User));
            }

            (TokenDTO tokenDTO, RefreshToken refreshToken) = _tokenGenerator
                .GenerateToken(creationRefreshTokenDTO.Name,
                creationRefreshTokenDTO.UserRoleName, creationRefreshTokenDTO.Id);

            await SaveTokenAsync(refreshToken, cancellationToken);

            await _refreshTokenRepository.SaveChangesToDbAsync(cancellationToken);

            RefreshTokenCookie = refreshToken.Token;

            return tokenDTO;
        }
    }
}
