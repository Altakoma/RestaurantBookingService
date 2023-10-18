using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper) : base(catalogServiceDbContext, mapper)
        {
        }
    }
}
