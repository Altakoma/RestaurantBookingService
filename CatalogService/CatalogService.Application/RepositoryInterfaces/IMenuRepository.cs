using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<ICollection<Menu>> GetAllByRestaurantIdAsync(int id);
    }
}
