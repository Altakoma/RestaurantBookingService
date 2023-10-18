namespace BookingService.Domain.Interfaces.Services.Base
{
    public interface IBaseService<T>
    {
        Task<U> InsertAsync<U>(T item, CancellationToken cancellationToken);
        Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken);
    }
}
