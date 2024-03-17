namespace BookingService.Domain.Exceptions
{
    public class BadConfigurationProvidedException : Exception
    {
        public BadConfigurationProvidedException(string message) : base(message)
        {
        }
    }
}
