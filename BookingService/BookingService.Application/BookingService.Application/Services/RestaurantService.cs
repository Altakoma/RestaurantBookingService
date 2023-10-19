using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    internal class RestaurantService : BaseService<Restaurant>, IBaseService
    {
        public RestaurantService(IRestaurantRepository restaurantRepository,
            IMapper mapper) : base(restaurantRepository, mapper)
        {
        }
    }
}
