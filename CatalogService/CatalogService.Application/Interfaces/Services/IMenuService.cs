using CatalogService.Application.DTOs.Menu;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IMenuService : IBaseMenuService
    {
        Task<T> InsertAsync<T>(MenuDTO menuDTO, CancellationToken cancellationToken);
        Task<T> UpdateAsync<T>(int id, MenuDTO menuDTO, CancellationToken cancellationToken);
    }
}
