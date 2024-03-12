using AutoMapper;
using BookingService.Application.DTOs.Booking;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.MappingProfiles;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data.Repositories;
using BookingService.Tests.Fakers;
using BookingService.Tests.RepositoryTests.Base;

namespace BookingService.Tests.RepositoryTests
{
    public class BookingRepositoryTests : BaseRepositoryTests<Booking>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingRepositoryTests() : base()
        {
            _bookingRepository = new BookingRepository(_dbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new BookingProfile())));

            _repository = _bookingRepository;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadBookingDTOs()
        {
            //Arrange
            var bookings = new List<Booking> { BookingDataFaker.GetFakedBooking() };
            IQueryable<Booking> bookingQuery = bookings.AsQueryable();

            var bookingReadDTOs = _mapper.Map<List<ReadBookingDTO>>(bookings);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(bookingQuery, bookingReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsBookings()
        {
            //Arrange
            var bookings = new List<Booking> { BookingDataFaker.GetFakedBooking() };
            IQueryable<Booking> bookingQuery = bookings.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(bookingQuery, bookings);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadBookingDTO()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();
            IQueryable<Booking> bookingQuery = new List<Booking> { booking }.AsQueryable();

            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(booking.Id, bookingQuery, readBookingDTO);
        }

        [Fact]
        public async Task GetByIdAsync_WhenItemIsNotExisting_ReturnsReadBookingDTO()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();

            //Act
            //Assert
            await base.GetByIdAsync_WhenItemIsNotExisting_ReturnsEntity<ReadBookingDTO>(booking.Id);
        }

        [Fact]
        public async Task InsertBookingAsync_ReturnsReadBookingDTO()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            //Act
            //Assert
            await base.InsertAsync_ReturnsEntity(booking, readBookingDTO);
        }

        [Fact]
        public async Task InsertBookingAsync_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();

            //Act
            //Assert
            await base.InsertAsync_WhenItIsNotFound_ThrowsNotFoundException<ReadBookingDTO>(booking);
        }

        [Fact]
        public async Task UpdateBooking__ReturnsReadBookingDTO()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            //Act
            //Assert
            await base.UpdateEntity_ReturnsEntity(booking, readBookingDTO);
        }

        [Fact]
        public async Task UpdateBooking__WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();

            //Act
            //Assert
            await base.UpdateEntity__WhenItIsNotFound_ThrowsNotFoundException<ReadBookingDTO>(booking);
        }

        [Fact]
        public async Task DeleteBooking_WhenEntityIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();

            var bookingQuery = new List<Booking> { booking }.AsQueryable();

            //Act
            //Assert
            await base.DeleteEntityAsync_WhenEntityIsExisting_SuccessfullyExecuted(
                bookingQuery, booking.Id);
        }

        [Fact]
        public async Task DeleteBookingAsync_WhenBookingIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var booking = BookingDataFaker.GetFakedBooking();

            var bookingQuery = new List<Booking>().AsQueryable();

            //Act
            //Assert
            await base.DeleteEntityAsync_WhenEntityIsNotExisting_ThrowsNotFoundException(
                bookingQuery, booking.Id);
        }
    }
}
