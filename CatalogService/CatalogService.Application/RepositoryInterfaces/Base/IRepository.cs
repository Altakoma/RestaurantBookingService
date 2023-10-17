namespace CatalogService.Application.RepositoryInterfaces.Base
{
    public interface IRepository<T>
    {
        Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken);
        Task<T> InsertAsync(T item, CancellationToken cancellationToken);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
        void Update(T item);
        void Delete(T item);
    }
}
