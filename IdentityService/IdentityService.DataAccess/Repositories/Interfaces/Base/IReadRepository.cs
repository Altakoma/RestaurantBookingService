namespace IdentityService.DataAccess.Repositories.Interfaces.Base
{
    public interface IReadRepository<T>
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}
