using CatalogService.Domain.Interfaces.Services.Base;

namespace CatalogService.Domain.Interfaces.Services
{
    public interface IBaseFoodTypeService : IBaseService
    {
        Task<T> ExecuteAndCheckAsync<T>(Func<Task<T>> function, CancellationToken cancellationToken);
    }
}
