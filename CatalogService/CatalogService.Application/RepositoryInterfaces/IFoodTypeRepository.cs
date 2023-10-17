using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
