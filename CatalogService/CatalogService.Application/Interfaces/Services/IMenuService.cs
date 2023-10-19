using CatalogService.Application.DTOs.Menu;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IMenuService : IBaseMenuService
    {
        Task<T> ExecuteAndCheckEmployeeAsync<T>(Func<Task<T>> function,
                    MenuDTO menuDTO, CancellationToken cancellationToken);
    }
}
