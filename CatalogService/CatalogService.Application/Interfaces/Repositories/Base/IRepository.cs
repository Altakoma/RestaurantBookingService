namespace CatalogService.Application.Interfaces.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken);
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task<T> InsertAsync(T item, CancellationToken cancellationToken);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
        void Update(T item);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        void Delete(T item);
    }
}
