namespace OrderService.Domain.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string key, Type type, string message)
            : base(string.Format(message, key, type.Name, type.Name))
        {
        }
    }
}
