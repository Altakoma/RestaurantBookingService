using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;

namespace CatalogService.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepository,
            IRestaurantRepository restaurantRepository,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
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

        public async Task<ICollection<ReadMenuDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadMenuDTO> readMenuDTOs =
                await _menuRepository.GetAllAsync<ReadMenuDTO>(cancellationToken);

            return readMenuDTOs;
        }

        public async Task<ICollection<ReadMenuDTO>> GetAllByRestaurantIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ICollection<ReadMenuDTO> readMenuDTOs =await _menuRepository
                .GetAllByRestaurantIdAsync<ReadMenuDTO>(id, cancellationToken);

            return readMenuDTOs;
        }

        public async Task<ReadMenuDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO? readMenuDTO =
                await _menuRepository.GetByIdAsync<ReadMenuDTO>(id, cancellationToken);

            if (readMenuDTO is null)
            {
                throw new NotFoundException(nameof(Menu),
                    id.ToString(), typeof(Menu));
            }

            return readMenuDTO;
        }

        public async Task<ReadMenuDTO> InsertAsync(InsertMenuDTO item,
            CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Menu>(item);

            menu = await _menuRepository
                         .InsertAsync(menu, cancellationToken);

            bool isInserted = await _menuRepository
                                    .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    menu.Id.ToString(), typeof(Menu));
            }

            ReadMenuDTO? readMenuDTO =
                await _menuRepository.GetByIdAsync<ReadMenuDTO>(menu.Id, cancellationToken);

            if (readMenuDTO is null)
            {
                throw new NotFoundException(nameof(Menu), menu.Id.ToString(),
                    typeof(Menu));
            }

            return readMenuDTO;
        }

        public async Task<ReadMenuDTO> UpdateAsync(int id,
            UpdateMenuDTO item, CancellationToken cancellationToken)
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

            ReadMenuDTO? readMenuDTO = 
                await _menuRepository.GetByIdAsync<ReadMenuDTO>(id, cancellationToken);

            if (readMenuDTO is null)
            {
                throw new NotFoundException(nameof(Menu), id.ToString(),
                    typeof(Menu));
            }

            return readMenuDTO;
        }
    }
}
