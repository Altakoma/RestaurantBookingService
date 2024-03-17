using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<ICollection<U>> GetAllByRestaurantIdAsync<U>(int id, CancellationToken cancellationToken);
    }
}
