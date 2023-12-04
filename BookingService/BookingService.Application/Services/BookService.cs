using AutoMapper;
using BookingService.Application.DTOs.Booking;
using BookingService.Application.Enums.HubMessages;
using BookingService.Application.Interfaces.HubServices;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.Services.Base;
using BookingService.Application.TokenParsers.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BookingService.Application.Services
{
    public class BookService : BaseService<Booking>, IBookService
    {
        private readonly ITokenParser _tokenParser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookingHubService _bookingHubService;

        public BookService(IBookingRepository bookingRepository,
            ITokenParser tokenParser,
            IHttpContextAccessor httpContextAccessor,
            IBookingHubService bookingHubService,
            IMapper mapper) : base(bookingRepository, mapper)
        {
            _tokenParser = tokenParser;
            _httpContextAccessor = httpContextAccessor;
            _bookingHubService = bookingHubService;
        }

        public async Task<T> UpdateAsync<T>(int id, UpdateBookingDTO updateItemDTO,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser.ParseSubjectId(_httpContextAccessor?
                                                        .HttpContext?.Request.Headers);

            Booking booking = _mapper.Map<Booking>(updateItemDTO);
            booking.ClientId = subjectId;

            T itemDTO = await base.UpdateAsync<Booking, T>(id, booking, cancellationToken);

            await _bookingHubService.SendBookingMessageAsync(HubMessageType.Update, itemDTO);

            return itemDTO;
        }

        public async Task<T> InsertAsync<T>(InsertBookingDTO insertItemDTO,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser.ParseSubjectId(_httpContextAccessor?
                                                        .HttpContext?.Request.Headers);

            Booking booking = _mapper.Map<Booking>(insertItemDTO);
            booking.ClientId = subjectId;

            T itemDTO = await base.InsertAsync<Booking, T>(booking, cancellationToken);

            await _bookingHubService.SendBookingMessageAsync(HubMessageType.Insert, itemDTO);

            return itemDTO;
        }

        public override async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser.ParseSubjectId(_httpContextAccessor?
                                                        .HttpContext?.Request.Headers);

            var booking = await GetByIdAsync<Booking>(id, cancellationToken);

            if (booking.ClientId != subjectId)
            {
                throw new AuthorizationException(subjectId.ToString(),
                    ExceptionMessages.AuthorizationExceptionMessage);
            }

            await base.DeleteAsync(id, cancellationToken);

            await _bookingHubService.SendBookingMessageAsync(HubMessageType.Delete, id);
        }
    }
}
