namespace IdentityService.DataAccess.Repositories.Interfaces.Base
{
    public interface IRepository<T, U> : IReadRepository<T>, IWriteRepository<U>
    {
    }
}
