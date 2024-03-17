using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.Services
{
    public class RestaurantService : BaseService<Restaurant>, IRestaurantService
    {
        public RestaurantService(IRestaurantRepository restaurantRepository,
            IMapper mapper) : base(restaurantRepository, mapper)
        {
        }
    }
}
