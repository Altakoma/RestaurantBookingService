namespace OrderService.Application.Interfaces.Repositories.Base
{
    public interface INoSqlRepository<T>
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAllAsync(int skipCount, int selectionAmount,
            CancellationToken cancellationToken);
        Task InsertAsync(T item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
