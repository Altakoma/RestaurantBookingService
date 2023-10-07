namespace IdentityService.BusinessLogic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string key, Type type) : base($"Object of type {type.Name} having {key} key wasn't found")
        {
        }
    }
}
