using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
    }
}
