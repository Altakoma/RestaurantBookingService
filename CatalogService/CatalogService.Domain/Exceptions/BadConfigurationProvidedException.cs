namespace CatalogService.Domain.Exceptions
{
    public class BadConfigurationProvidedException : Exception
    {
        public BadConfigurationProvidedException(string message) : base(message)
        {
        }
    }
}
