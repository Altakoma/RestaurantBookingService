using BookingService.Application.TokenParsers.Interfaces;
using BookingService.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookingService.Application.TokenParsers
{
    public class JwtTokenParser : ITokenParser
    {
        public int ParseSubjectId(IHeaderDictionary? headers)
        {
            if (headers is null)
            {
                throw new NullReferenceException(string.Format
                    (ExceptionMessages.NullReferenceMessage, nameof(IHeaderDictionary)));
            }

            bool isHeaderExists = headers.TryGetValue("Authorization", out StringValues jwt);

            if (!isHeaderExists)
            {
                throw new NotFoundException("Authorization", typeof(IHeaderDictionary));
            }

            string token = jwt!;

            var tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token.Replace("Bearer ", ""));

            IEnumerable<Claim> claims = jwtToken.Claims;

            Claim subjectClaim = claims.Where(c => c.Type == "sub").FirstOrDefault()!;

            if (!int.TryParse(subjectClaim.Value, out int id))
            {
                throw new NotFoundException("sub", typeof(Claim));
            }

            return id;
        }
    }
}
