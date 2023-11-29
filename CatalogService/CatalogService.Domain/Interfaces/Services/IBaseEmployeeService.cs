using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services.Base;

namespace CatalogService.Domain.Interfaces.Services
{
    public interface IBaseEmployeeService : IBaseService
    {
        Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(int id, CancellationToken cancellationToken);
    }
}
