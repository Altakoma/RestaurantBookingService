using BookingService.Application.RepositoryInterfaces.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.RepositoryInterfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
