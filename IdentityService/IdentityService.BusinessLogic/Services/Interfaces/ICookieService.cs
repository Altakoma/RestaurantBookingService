namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface ICookieService
    {
        string GetCookieValue(string cookieName);
        void SetCookieValue(string cookieName, string cookieValue);
    }
}
