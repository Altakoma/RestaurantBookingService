namespace IdentityService.DataAccess.Repositories.Interfaces.Base
{
    public interface IReadRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
    }
}
