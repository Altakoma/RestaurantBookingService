using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository,
            IMapper mapper) : base(menuRepository, mapper)
        {
            _menuRepository = menuRepository;
        }

        public async Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(int id,
            CancellationToken cancellationToken)
        {
            ICollection<T> readMenuDTOs = await _menuRepository
                .GetAllByRestaurantIdAsync<T>(id, cancellationToken);

            return readMenuDTOs;
        }
    }
}
