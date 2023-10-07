namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> : ICreateReadRepository<T>, IUpdateDeleteRepository<T>
    {
    }
}
