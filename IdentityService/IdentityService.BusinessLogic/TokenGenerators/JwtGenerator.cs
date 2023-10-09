using IdentityService.BusinessLogic.DTOs.TokenDTOs;
using IdentityService.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.BusinessLogic.TokenGenerators
{
    public class JwtGenerator : ITokenGenerator
    {
        private SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public (TokensDTO, RefreshToken) GenerateToken(string name,
            string roleName, int userId)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Name, name),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),

                IssuedAt = DateTime.Now.ToUniversalTime(),

                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(
                    Environment.GetEnvironmentVariable("JWTExpirationTime")!)),

                SigningCredentials = new SigningCredentials(
                    GetSymmetricSecurityKey(Environment.GetEnvironmentVariable("JWTSecret")!),
                    SecurityAlgorithms.HmacSha256),
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = GetRandomRefreshToken(20),
                IsUsed = false,
                isRevoked = false,
                AddedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMinutes(20),
            };

            var tokenDTO = new TokensDTO
            {
                EncodedToken = jwtTokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
            };

            return (tokenDTO, refreshToken);
        }

        private string GetRandomRefreshToken(int length)
        {
            var random = new Random();
            var chars = "XCVBNMASDFGHJKLQWERTYUIOP123456789zxcvbnmmasdfghjklqwertyuiop_";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
