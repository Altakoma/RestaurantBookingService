using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Data.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(BookingServiceDbContext bookingServiceDbContext,
            IMapper mapper) : base(bookingServiceDbContext, mapper)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Restaurant? restaurant = await _bookingServiceDbContext.Restaurants
                .FirstOrDefaultAsync(restaurant => restaurant.Id == id,
                                     cancellationToken);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), id.ToString(),
                    typeof(Restaurant));
            }

            Delete(restaurant);
        }

        public async Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken)
        {
            U? readRestaurantDTO = await _mapper.ProjectTo<U>(
                _bookingServiceDbContext.Restaurants.Where(restaurant => restaurant.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readRestaurantDTO;
        }
    }
}
