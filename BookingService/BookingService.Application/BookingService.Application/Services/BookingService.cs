using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    public class BookingService : BaseService<Booking>, IBaseService
    {
        public BookingService(IBookingRepository bookingRepository,
            IMapper mapper) : base(bookingRepository, mapper)
        {
        }
    }
}
