using CatalogService.Application.DTOs.Menu;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IMenuService : IBaseMenuService
    {
        Task<T> InsertAsync<T>(InsertMenuDTO insertItemDTO, CancellationToken cancellationToken);
        Task<T> UpdateAsync<T>(int id, UpdateMenuDTO updateItemDTO, CancellationToken cancellationToken);
    }
}
