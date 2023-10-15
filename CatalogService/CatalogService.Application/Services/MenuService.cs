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
            IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            Menu? menu = await _menuRepository.GetByIdAsync(id);

            if (menu is null)
            {
                throw new NotFoundException(nameof(Menu), id.ToString(), typeof(Menu));
            }

            bool isDeleted = await _menuRepository.DeleteAsync(menu);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(Menu));
            }
        }

        public async Task<ICollection<ReadMenuDTO>> GetAllAsync()
        {
            ICollection<Menu> menu = await _menuRepository.GetAllAsync();

            var readMenuDTOs = _mapper.Map<ICollection<ReadMenuDTO>>(menu);

            return readMenuDTOs;
        }

        public async Task<ICollection<ReadMenuDTO>> GetAllByRestaurantIdAsync(int id)
        {
            ICollection<Menu> menu = await _menuRepository.GetAllByRestaurantIdAsync(id);

            var readMenuDTOs = _mapper.Map<ICollection<ReadMenuDTO>>(menu);

            return readMenuDTOs;
        }

        public async Task<ReadMenuDTO> GetByIdAsync(int id)
        {
            Menu? menu = await _menuRepository.GetByIdAsync(id);

            if (menu is null)
            {
                throw new NotFoundException(nameof(Menu), id.ToString(), typeof(Menu));
            }

            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            return readMenuDTO;
        }

        public async Task<ReadMenuDTO> InsertAsync(InsertMenuDTO item)
        {
            var menu = _mapper.Map<Menu>(item);

            (menu, bool isInserted) = await _menuRepository
                .InsertAsync(menu);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    menu.Id.ToString(), typeof(Menu));
            }

            menu = await _menuRepository.GetByIdAsync(menu.Id);

            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            return readMenuDTO;
        }

        public async Task<ReadMenuDTO> UpdateAsync(int id, UpdateMenuDTO item)
        {
            var menu = _mapper.Map<Menu>(item);
            menu.Id = id;

            bool isUpdated = await _menuRepository.UpdateAsync(menu);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync), id.ToString(),
                    typeof(Menu));
            }

            menu = await _menuRepository.GetByIdAsync(id);

            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            return readMenuDTO;
        }
    }
}
