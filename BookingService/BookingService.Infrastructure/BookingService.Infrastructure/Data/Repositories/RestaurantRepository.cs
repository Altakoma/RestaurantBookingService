using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Infrastructure.Data.Repositories.Base;

namespace BookingService.Infrastructure.Data.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(BookingServiceDbContext bookingServiceDbContext,
            IMapper mapper) : base(bookingServiceDbContext, mapper)
        {
        }
    }
}
