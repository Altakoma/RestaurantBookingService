using CatalogService.Domain.Interfaces.Services.Base;

namespace CatalogService.Domain.Interfaces.Services
{
    public interface IEmployeeService : IService
    {
        Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(int id, CancellationToken cancellationToken);
    }
}
