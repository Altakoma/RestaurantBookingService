using IdentityService.BusinessLogic.DTOs.TokenDTOs;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityService.BusinessLogic.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            TokenValidationParameters tokenValidationParams,
            ITokenGenerator tokenGenerator)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenValidationParameters = tokenValidationParams;
            _tokenGenerator = tokenGenerator;
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

        public async Task<TokensDTO?> VerifyAndGenerateToken(TokensDTO tokensDTO)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenInVerification = jwtTokenHandler
                .ValidateToken(tokensDTO.EncodedToken,
                _tokenValidationParameters, out SecurityToken validatedToken);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.CurrentCultureIgnoreCase);

                if (!result)
                {
                    return null;
                }

                var name = jwtSecurityToken.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)!.Value;
                var roleName = jwtSecurityToken.Claims
                    .FirstOrDefault(x => x.Type == "role")!.Value;
                var userId = int.Parse(jwtSecurityToken.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)!.Value);

                (tokensDTO, var refreshToken) = _tokenGenerator
                    .GenerateToken(name, roleName, userId);

                await SaveToken(refreshToken);

                return tokensDTO;
            }
            else
            {
                return null;
            }
        }
    }
}
