using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    internal class RestaurantService : BaseService<Restaurant>, IService<Restaurant>
    {
        public RestaurantService(IRestaurantRepository restaurantRepository,
            IMapper mapper) : base(restaurantRepository, mapper)
        {
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<U> GetByIdAsync<U>(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<U> UpdateAsync<U>(int id, Restaurant item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
