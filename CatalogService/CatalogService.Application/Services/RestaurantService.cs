using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class RestaurantService : BaseService<Restaurant>, IRestaurantService
    {
        public RestaurantService(IRestaurantRepository restaurantRepository,
            IMapper mapper) : base(restaurantRepository, mapper)
        {
        }
    }
}
