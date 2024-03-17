using BookingService.Application.DTOs.Booking;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Interfaces.Services
{
    public interface IBookingService : IBaseService
    {
        Task<T> UpdateAsync<T>(int id, UpdateBookingDTO updateItemDTO,
            CancellationToken cancellationToken);
        Task<T> InsertAsync<T>(InsertBookingDTO insertItemDTO,
            CancellationToken cancellationToken);
    }
}
