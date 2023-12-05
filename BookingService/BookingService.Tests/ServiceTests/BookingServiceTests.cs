using AutoMapper;
using BookingService.Application.DTOs.Booking;
using BookingService.Application.Enums.HubMessages;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.MappingProfiles;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Tests.Fakers;
using BookingService.Tests.Mocks.HttpContextAccessors;
using BookingService.Tests.Mocks.HubServices;
using BookingService.Tests.Mocks.Repositories;
using BookingService.Tests.Mocks.TokenParsers;
using BookingService.Tests.ServiceTests.Base;
using FluentAssertions;
using Moq;

namespace BookingService.Tests.ServiceTests
{
    public class BookingServiceTests : BaseServiceTests<IBookingRepository, Booking>
    {
        private readonly BookingRepositoryMock _bookingRepositoryMock;
        private readonly HttpContextAccessorMock _httpContextAccessorMock;
        private readonly TokenParserMock _tokenParserMock;
        private readonly BookingHubServiceMock _bookingHubServiceMock;

        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingServiceTests() : base()
        {
            _bookingRepositoryMock = new();
            _httpContextAccessorMock = new();
            _tokenParserMock = new();
            _bookingHubServiceMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
            configure.AddProfile(new BookingProfile())));

            _bookingService = new Application.Services.BookingService(
                _bookingRepositoryMock.Object, _tokenParserMock.Object,
                _httpContextAccessorMock.Object, _bookingHubServiceMock.Object,
                _mapperMock.Object);

            _baseRepositoryMock = _bookingRepositoryMock;
            _baseService = _bookingService;
        }

        [Fact]
        public async Task GetBookingByIdAsync_ReturnsReadBookingDTO()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(booking.Id, readBookingDTO);
        }

        [Fact]
        public async Task GetAllBookingsAsync_ReturnsReadBookingDTOs()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var bookings = new List<Booking> { booking };
            var readBookingDTOs = _mapper.Map<List<ReadBookingDTO>>(bookings);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(readBookingDTOs);
        }

        [Fact]
        public async Task InsertBooking_ReturnsReadBookingDTO()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var insertBookingDTO = _mapper.Map<InsertBookingDTO>(booking);
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            _tokenParserMock.MockParseSubjectId(booking.ClientId);

            _mapperMock.MockMap(insertBookingDTO, booking)
                       .MockMap(booking, booking);

            _bookingHubServiceMock.MockSendBookingMessageAsync(HubMessageType.Insert, readBookingDTO);

            _baseRepositoryMock.MockInsertAsync(booking, readBookingDTO);

            //Act
            var result = await _bookingService.InsertAsync<ReadBookingDTO>(insertBookingDTO,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readBookingDTO);

            _tokenParserMock.Verify();
            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
            _bookingHubServiceMock.Verify();
        }

        [Fact]
        public async Task DeleteBooking_WhenItIsSaved()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();

            _tokenParserMock.MockParseSubjectId(booking.ClientId);

            _bookingRepositoryMock.MockGetByIdAsync(booking.Id, booking);

            _bookingHubServiceMock.MockSendBookingMessageAsync(HubMessageType.Delete, booking.Id);

            //Act
            await base.DeleteAsync_WhenItIsSaved(booking);

            //Assert
            _tokenParserMock.Verify();
            _bookingRepositoryMock.Verify();
            _bookingHubServiceMock.Verify();
        }

        [Fact]
        public async Task DeleteBooking_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();

            _tokenParserMock.MockParseSubjectId(booking.ClientId);

            _bookingRepositoryMock.MockGetByIdAsync(booking.Id, booking);

            //Act
            await base.DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(booking);

            //Assert
            _tokenParserMock.Verify();
            _bookingRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteBooking_WhenInitianorIsNotBookingOwner_AuthorizationException()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();

            _tokenParserMock.MockParseSubjectId(It.Is<int>(number => number != booking.ClientId));

            _bookingRepositoryMock.MockGetByIdAsync(booking.Id, booking);

            //Act
            var result = _bookingService.DeleteAsync(booking.Id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _bookingRepositoryMock.Verify();
        }

        [Fact]
        public async Task UpdateBooking_ReturnsReadBookingDTO()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var updateBookingDTO = _mapper.Map<UpdateBookingDTO>(booking);
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            _tokenParserMock.MockParseSubjectId(booking.ClientId);

            _mapperMock.MockMap(updateBookingDTO, booking)
                       .MockRefMap(booking, booking);

            _bookingHubServiceMock.MockSendBookingMessageAsync(HubMessageType.Update, readBookingDTO);

            _baseRepositoryMock.MockUpdateAsync(booking, readBookingDTO);

            _bookingRepositoryMock.MockGetByIdAsync(booking.Id, booking);

            //Act
            var result = await _bookingService.UpdateAsync<ReadBookingDTO>(booking.Id, 
                updateBookingDTO, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readBookingDTO);

            _tokenParserMock.Verify();
            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
            _bookingHubServiceMock.Verify();
        }

        [Fact]
        public async Task UpdateBooking_WhenInitianorIsNotBookingOwner_AuthorizationException()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var updateBookingDTO = _mapper.Map<UpdateBookingDTO>(booking);
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            _tokenParserMock.MockParseSubjectId(It.Is<int>(number => number != booking.ClientId));

            _bookingRepositoryMock.MockGetByIdAsync(booking.Id, booking);

            //Act
            var result = _bookingService.UpdateAsync<ReadBookingDTO>(booking.Id,
                updateBookingDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _baseRepositoryMock.Verify();
        }
    }
}
