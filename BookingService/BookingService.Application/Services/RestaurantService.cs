using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services;

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
