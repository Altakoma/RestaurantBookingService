using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<ICollection<U>> GetAllByRestaurantIdAsync<U>(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
    }
}
