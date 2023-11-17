namespace CatalogService.Application.Redis.Interfaces.Base
{
    public interface ICacheAccessor
    {
        Task<U> GetByResourceIdAsync<U>(string resourceId, CancellationToken cancellationToken);
        Task DeleteResourceByIdAsync(string resourceId, CancellationToken cancellationToken);
    }
}
