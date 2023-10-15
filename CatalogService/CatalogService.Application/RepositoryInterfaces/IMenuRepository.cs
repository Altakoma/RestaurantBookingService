using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IMenuRepository : IRepository<Menu, ReadMenuDTO>
    {
        Task<ICollection<ReadMenuDTO>> GetAllByRestaurantIdAsync(int id, CancellationToken cancellationToken);
    }
}
