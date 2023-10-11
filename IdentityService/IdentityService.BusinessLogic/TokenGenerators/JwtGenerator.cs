﻿using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.BusinessLogic.TokenGenerators
{
    public class JwtGenerator : ITokenGenerator
    {
        private const string CharsForGeneration = "XCVBNMASDFGHJKLQWERTYUIOP123456789zxcvbnmmasdfghjklqwertyuiop_";

        private SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public (TokenDTO, RefreshToken) GenerateToken(string name,
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
                Token = GetRandomRefreshToken(40),
                isRevoked = false,
                AddedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMinutes(20),
            };

            var tokenDTO = new TokenDTO
            {
                EncodedToken = jwtTokenHandler.WriteToken(token),
            };

            return (tokenDTO, refreshToken);
        }

        private string GetRandomRefreshToken(int length)
        {
            var random = new Random();

            return new string(Enumerable.Repeat(CharsForGeneration, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
