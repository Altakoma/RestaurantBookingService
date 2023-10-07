namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface ICreateUpdateDeleteRepository<T>
    {
        Task<bool> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
    }
}
