using AutoMapper;
using BookingService.Application.DTOs.Booking;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Application.TokenParsers.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace BookingService.Application.Services
{
    public class BookService : BaseService<Booking>, IBookService
    {
        private readonly ITokenParser _tokenParser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(IBookingRepository bookingRepository,
            ITokenParser tokenParser,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(bookingRepository, mapper)
        {
            _tokenParser = tokenParser;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T> UpdateAsync<T>(int id, UpdateBookingDTO updateItemDTO,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser.ParseSubjectId(_httpContextAccessor?
                                                        .HttpContext?.Request.Headers);

            Booking booking = _mapper.Map<Booking>(updateItemDTO);
            booking.ClientId = subjectId;

            return await base.UpdateAsync<Booking, T>(id, booking, cancellationToken);
        }

        public async Task<T> InsertAsync<T>(InsertBookingDTO insertItemDTO,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser.ParseSubjectId(_httpContextAccessor?
                                                        .HttpContext?.Request.Headers);

            Booking booking = _mapper.Map<Booking>(insertItemDTO);
            booking.ClientId = subjectId;

            return await base.InsertAsync<Booking, T>(booking, cancellationToken);
        }
    }
}
