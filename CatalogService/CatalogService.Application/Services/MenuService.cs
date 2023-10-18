using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public MenuService(IMenuRepository menuRepository,
            IRestaurantRepository restaurantRepository,
            IMapper mapper) : base(menuRepository, mapper)
        {
            _menuRepository = menuRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            ReadMenuDTO? readMenuDTO =
                await _menuRepository.GetByIdAsync<ReadMenuDTO>(id, cancellationToken);

            if (readMenuDTO is null)
            {
                throw new NotFoundException(nameof(Menu), id.ToString(),
                    typeof(Menu));
            }

            await _menuRepository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _menuRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(Menu));
            }
        }

        public async Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(int id,
            CancellationToken cancellationToken)
        {
            ICollection<T> readMenuDTOs = await _menuRepository
                .GetAllByRestaurantIdAsync<T>(id, cancellationToken);

            return readMenuDTOs;
        }

        public async Task<T> GetByIdAsync<T>(int id, CancellationToken cancellationToken)
        {
            T? readMenuDTO = await _menuRepository.GetByIdAsync<T>(id, cancellationToken);

            if (readMenuDTO is null)
            {
                throw new NotFoundException(nameof(Menu),
                    id.ToString(), typeof(Menu));
            }

            return readMenuDTO;
        }

        public async Task<T> UpdateAsync<U,T>(int id, U item,
            CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Menu>(item);
            menu.Id = id;

            _menuRepository.Update(menu);

            bool isUpdated = await _menuRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync),
                    id.ToString(), typeof(Menu));
            }

            T? readMenuDTO = await _menuRepository
                .GetByIdAsync<T>(id, cancellationToken);

            if (readMenuDTO is null)
            {
                throw new NotFoundException(nameof(Menu), id.ToString(),
                    typeof(Menu));
            }

            return readMenuDTO;
        }
    }
}
