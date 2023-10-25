namespace OrderService.Application.Interfaces.Repositories.Base
{
    public interface IReadRepository<T>
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}
