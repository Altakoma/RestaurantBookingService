namespace IdentityService.DataAccess.Repositories.Interfaces.Base
{
    public interface IRepository<T>
    {
        Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken);
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task<T> InsertAsync(T item, CancellationToken cancellationToken);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
        void Update(T item);
        void Delete(T item);
    }
}
