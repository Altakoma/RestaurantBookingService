using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace IdentityService.BusinessLogic.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

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

        public async Task DeleteAsync(int id)
        {
            var refreshToken = await _refreshTokenRepository.GetByUserIdAsync(id);

            if (refreshToken is null)
            {
                throw new NotFoundException(id.ToString(), typeof(RefreshToken));
            }

            bool isDeleted = await _refreshTokenRepository.DeleteAsync(refreshToken);

            if (!isDeleted)
            {
                throw new DbOperationException(
                    nameof(DeleteAsync), id.ToString(), typeof(RefreshToken));
            }
        }

        public async Task<RefreshToken> GetByUserIdAsync(int id)
        {
            var token = await _refreshTokenRepository.GetByUserIdAsync(id);

            if (token is null)
            {
                throw new NotFoundException(id.ToString(), typeof(RefreshToken));
            }

            return token;
        }

        public async Task InsertAsync(RefreshToken item)
        {
            (var refreshToken, bool isInserted) = 
                await _refreshTokenRepository.InsertAsync(item);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    item.UserId.ToString(), typeof(RefreshToken));
            }
        }

        public async Task SaveTokenAsync(RefreshToken token)
        {
            if (await GetByUserIdAsync(token.UserId) is null)
            {
                await InsertAsync(token);
            }
            else
            {
                await UpdateAsync(token);
            }
        }

        public async Task UpdateAsync(RefreshToken item)
        {
            bool isUpdated = await _refreshTokenRepository.UpdateAsync(item);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync),
                    item.UserId.ToString(), typeof(RefreshToken));
            }
        }

        public void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(20),
            };

            _httpContextAccessor.HttpContext.Response
                .Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }

        public async Task<TokenDTO> VerifyAndGenerateTokenAsync()
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var refreshTokenString = _httpContextAccessor.HttpContext.Request
                .Cookies.FirstOrDefault(c => c.Key == "RefreshToken").Value;

            if (refreshTokenString is null)
            {
                throw new NotFoundException("RefreshToken", typeof(Cookie));
            }

            var user = await _refreshTokenRepository
                .GetUserByRefreshTokenAsync(refreshTokenString);

            if (user is null)
            {
                throw new NotFoundException(nameof(refreshTokenString), typeof(User));
            }

            (TokenDTO tokenDTO, RefreshToken refreshToken) = _tokenGenerator
                .GenerateToken(user.Name, user.UserRole.Name, user.Id);

            await SaveTokenAsync(refreshToken);

            SetRefreshTokenCookie(refreshToken.Token);

            return tokenDTO;
        }
    }
}
