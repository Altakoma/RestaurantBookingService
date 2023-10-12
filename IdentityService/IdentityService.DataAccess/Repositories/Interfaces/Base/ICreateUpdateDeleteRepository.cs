namespace IdentityService.DataAccess.Repositories.Interfaces.Base
{
    public interface ICreateUpdateDeleteRepository<T>
    {
        Task<(T, bool)> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
    }
}
