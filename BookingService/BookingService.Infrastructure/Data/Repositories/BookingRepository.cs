using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Data.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingServiceDbContext bookingServiceDbContext, 
            IMapper mapper) : base(bookingServiceDbContext, mapper)
        {
        }

        public async Task<bool> IsClientBookedTableAsync(int clientId, int bookingId,
            CancellationToken cancellationToken)
        {
            bool isClientBookedTable = await _bookingServiceDbContext.Bookings.AnyAsync(booking =>
            booking.ClientId == clientId && booking.Id == bookingId, cancellationToken);

            return isClientBookedTable;
        }
    }
}
