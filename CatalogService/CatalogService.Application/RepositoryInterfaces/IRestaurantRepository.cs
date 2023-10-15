using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant, ReadRestaurantDTO>
    {
    }
}
