using AutoMapper;
using BookingService.Application.RepositoryInterfaces;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    public class BookingService : BaseService<Booking>, IService<Booking>
    {
        public BookingService(IBookingRepository bookingRepository,
            IMapper mapper) : base(bookingRepository, mapper)
        {
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<U> GetByIdAsync<U>(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<U> UpdateAsync<U>(int id, Booking item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
