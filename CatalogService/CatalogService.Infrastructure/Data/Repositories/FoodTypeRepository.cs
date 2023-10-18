using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class FoodTypeRepository : BaseRepository<FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper) : base(catalogServiceDbContext, mapper)
        {
        }
    }
}
