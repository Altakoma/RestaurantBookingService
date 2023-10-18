using BookingService.Application.RepositoryInterfaces.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.RepositoryInterfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
    }
}
