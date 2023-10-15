namespace CatalogService.Application.RepositoryInterfaces.Base
{
    public interface IRepository<T, U>
    {
        Task<(T, bool)> InsertAsync(T item);
        void Update(T item);
        void Delete(T item);
        Task<U?> GetByIdAsync(int id);
        Task<ICollection<U>> GetAllAsync();
    }
}
