namespace CatalogService.Domain.Interfaces.Services.Base
{
    public interface IBaseService
    {
        Task<T> InsertAsync<U, T>(U insertItemDTO, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAllAsync<T>(CancellationToken cancellationToken);
    }
}
