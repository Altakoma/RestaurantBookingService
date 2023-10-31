namespace OrderService.Domain.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string key, string message)
            : base(string.Format(message, key))
        {
        }
    }
}
