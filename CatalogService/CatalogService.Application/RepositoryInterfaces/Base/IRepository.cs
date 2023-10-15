namespace CatalogService.Application.RepositoryInterfaces.Base
{
    public interface IRepository<T, U> : IWriteRepository<T>
    {
        Task<U?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<U>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
