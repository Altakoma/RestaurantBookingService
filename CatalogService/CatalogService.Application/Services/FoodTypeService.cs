using AutoMapper;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class FoodTypeService : BaseService<FoodType>, IFoodTypeService
    {
        public FoodTypeService(IFoodTypeRepository foodTypeRepository,
            IMapper mapper) : base(foodTypeRepository, mapper)
        {
        }
    }
}
