namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface ICreateReadRepository<T>
    {
        Task<bool> InsertAsync(T item);
        Task<T?> GetByIdAsync(int id);
        Task<ICollection<T>?> GetAllAsync();
    }
}
