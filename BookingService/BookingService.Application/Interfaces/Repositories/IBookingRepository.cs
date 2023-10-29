using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.Interfaces.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
    }
}
