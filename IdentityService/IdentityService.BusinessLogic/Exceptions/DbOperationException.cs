using IdentityService.DataAccess.Exceptions;

namespace IdentityService.BusinessLogic.Exceptions
{
    public class DbOperationException : Exception
    {
        public DbOperationException(string methodName, string key, Type type)
            : base(string.Format(ExceptionMessages.DbOperationExceptionMessage, methodName, type.Name, key))
        {
        }
    }
}
