namespace OrderService.Application.Interfaces.Repositories.Base
{
    public interface ISqlRepository<T>
    {
        Task<U> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task<U> InsertAsync<U>(T item, CancellationToken cancellationToken);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
        Task<U> UpdateAsync<U>(T item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        void Delete(T item);
    }
}
