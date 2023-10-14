namespace IdentityService.DataAccess.Repositories.Interfaces.Base
{
    public interface IWriteRepository<T>
    {
        Task<T> InsertAsync(T item, CancellationToken cancellationToken);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
        void Update(T item);
        void Delete(T item);
    }
}
