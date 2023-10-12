namespace IdentityService.DataAccess.Repositories.Interfaces.Base
{
    public interface IRepository<T> : IReadRepository<T>, ICreateUpdateDeleteRepository<T>
    {
    }
}
