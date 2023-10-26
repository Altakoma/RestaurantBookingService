namespace BookingService.Domain.Interfaces.Services.Base
{
    public interface IBaseService
    {
        Task<T> InsertAsync<U, T>(U insertItemDTO, CancellationToken cancellationToken);
        Task<T> UpdateAsync<U, T>(int id, U item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<T> GetByIdAsync<T>(int id, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAllAsync<T>(CancellationToken cancellationToken);
    }
}
