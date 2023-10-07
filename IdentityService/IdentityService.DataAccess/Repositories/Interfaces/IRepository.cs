namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> : IReadRepository<T>, ICreateUpdateDeleteRepository<T>
    {
    }
}
