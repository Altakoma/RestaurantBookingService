namespace CatalogService.Application.RepositoryInterfaces.Base
{
    public interface IRepository<T>
    {
        Task<(T, bool)> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
        Task<T?> GetByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
    }
}
