using IdentityService.BusinessLogic.DTOs.TokenDTOs;
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

            await _refreshTokenRepository.DeleteAsync(refreshToken);
        }

        public async Task<RefreshToken?> GetByUserId(int id)
        {
            var token = await _refreshTokenRepository.GetByUserIdAsync(id);
            return token;
        }

        public async Task InsertAsync(RefreshToken item)
        {
            await _refreshTokenRepository.InsertAsync(item);
        }

        public async Task SaveToken(RefreshToken token)
        {
            if (await GetByUserId(token.UserId) is null)
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
            await _refreshTokenRepository.UpdateAsync(item);
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

        public async Task<TokenDTO?> VerifyAndGenerateToken()
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var refreshTokenString = _httpContextAccessor.HttpContext.Request
                .Cookies.FirstOrDefault(c => c.Key == "RefreshToken").Value;

            if (refreshTokenString is null)
            {
                throw new NotFoundException("RefreshToken", typeof(Cookie));
            }

            var user = await _refreshTokenRepository
                .GetUserByRefreshToken(refreshTokenString);

            if (user is null)
            {
                throw new NotFoundException("refreshToken", typeof(User));
            }

            (TokenDTO tokenDTO, RefreshToken refreshToken) = _tokenGenerator
                .GenerateToken(user.Name, user.UserRole.Name, user.Id);

            await SaveToken(refreshToken);

            SetRefreshTokenCookie(refreshToken.Token);

            return tokenDTO;
        }
    }
}
