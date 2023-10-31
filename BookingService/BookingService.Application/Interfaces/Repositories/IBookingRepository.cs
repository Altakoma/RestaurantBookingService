using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.Interfaces.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<bool> IsClientBookedTableAsync(int clientId, int tableId,
            CancellationToken cancellationToken);
    }
}
