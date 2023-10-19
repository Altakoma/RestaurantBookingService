using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services;

namespace BookingService.Application.Services
{
    public class BookService : BaseService<Booking>, IBookService
    {
        public BookService(IBookingRepository bookingRepository,
            IMapper mapper) : base(bookingRepository, mapper)
        {
        }
    }
}
