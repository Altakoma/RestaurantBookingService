namespace OrderService.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string key, Type type)
            : base(string.Format(ExceptionMessages.NotFoundExceptionMessage, type.Name, key))
        {
        }
    }
}
