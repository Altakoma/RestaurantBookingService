namespace BookingService.Domain.Interfaces.Services.Base
{
    public interface IService<T> : IBaseService<T>
    {
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<U> UpdateAsync<U>(int id, T item, CancellationToken cancellationToken);
        Task<U> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
    }
}
