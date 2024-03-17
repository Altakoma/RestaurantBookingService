namespace BookingService.Application.Interfaces.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken);
        Task<U> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task<U> InsertAsync<U>(T item, CancellationToken cancellationToken);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
        Task<U> UpdateAsync<U>(T item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        void Delete(T item);
    }
}
