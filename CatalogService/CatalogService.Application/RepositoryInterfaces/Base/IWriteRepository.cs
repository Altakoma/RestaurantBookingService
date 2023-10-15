namespace CatalogService.Application.RepositoryInterfaces.Base
{
    public interface IWriteRepository<T>
    {
        Task<T> InsertAsync(T item, CancellationToken cancellationToken);
        void Update(T item);
        void Delete(T item);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
    }
}