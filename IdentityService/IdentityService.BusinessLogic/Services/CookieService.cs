using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.DataAccess.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace IdentityService.BusinessLogic.Services
{
    public class CookieService : ICookieService
    {
        public const string RefreshTokenCookieName = "RefreshToken";
        public const string UserIdCookieName = "UserId";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCookieValue(string cookieName)
        {
            string? cookieValue = _httpContextAccessor.HttpContext?.Request
            .Cookies.FirstOrDefault(c => c.Key == cookieName).Value;

            if (cookieValue is null)
            {
                throw new NotFoundException(cookieName, typeof(Cookie));
            }

            return cookieValue;
        }

        public void SetCookieValue(string cookieName, string cookieValue)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.Now.AddMinutes(20),
                SameSite = SameSiteMode.Strict,
                Secure = false,
            };

            _httpContextAccessor.HttpContext?.Response
                .Cookies.Append(cookieName, cookieValue, cookieOptions);
        }
    }
}
