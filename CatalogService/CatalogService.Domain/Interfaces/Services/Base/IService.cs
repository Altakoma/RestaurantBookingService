namespace CatalogService.Domain.Interfaces.Services.Base
{
    public interface IService : IBaseService
    {
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<T> UpdateAsync<U, T>(int id, U item, CancellationToken cancellationToken);
        Task<T> GetByIdAsync<T>(int id, CancellationToken cancellationToken);
    }
}
