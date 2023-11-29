using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.Interfaces.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<int> GetRestaurantIdByTableIdAsync(int id, CancellationToken cancellationToken);
    }
}
