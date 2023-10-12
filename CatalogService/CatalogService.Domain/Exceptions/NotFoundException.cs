namespace CatalogService.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string keyOwner, string key, Type type)
            : base((ExceptionMessages.NotFoundExceptionMessage, type.Name, key, keyOwner).ToString())
        {
        }
    }
}
