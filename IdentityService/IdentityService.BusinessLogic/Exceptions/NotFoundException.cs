namespace IdentityService.BusinessLogic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string key, Type type) 
            : base((ExceptionMessages.NotFoundExceptionMessage, type.Name, key).ToString())
        {
        }
    }
}
