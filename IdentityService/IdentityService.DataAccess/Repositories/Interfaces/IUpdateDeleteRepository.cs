namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IUpdateDeleteRepository<T>
    {
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
    }
}
