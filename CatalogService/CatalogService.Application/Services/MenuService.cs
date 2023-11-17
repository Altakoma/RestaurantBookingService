using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Services.Base;
using CatalogService.Application.TokenParsers.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Services
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ITokenParser _tokenParser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public MenuService(IMenuRepository menuRepository,
            IMapper mapper, IHttpContextAccessor httpContextAccessor,
            ITokenParser tokenParser, IEmployeeRepository employeeRepository,
            IRestaurantRepository restaurantRepository) : base(menuRepository, mapper)
        {
            _menuRepository = menuRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenParser = tokenParser;
            _employeeRepository = employeeRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(int id,
            CancellationToken cancellationToken)
        {
            ICollection<T> readMenuDTOs = await _menuRepository
                .GetAllByRestaurantIdAsync<T>(id, cancellationToken);

            return readMenuDTOs;
        }

        public new async Task<int> DeleteAsync(int id,
            CancellationToken cancellationToken)
        {
            MenuDTO? menuDTO = await _menuRepository
                .GetByIdAsync<MenuDTO>(id, cancellationToken);

            if (menuDTO is null)
            {
                throw new NotFoundException(nameof(Menu), id.ToString(), typeof(Menu));
            }

            await EnsureEmployeeValidOrThrowAsync(menuDTO, cancellationToken);

            return await base.DeleteAsync(id, cancellationToken);
        }

        public async Task<T> InsertAsync<T>(MenuDTO menuDTO,
            CancellationToken cancellationToken)
        {
            await EnsureEmployeeValidOrThrowAsync(menuDTO, cancellationToken);

            T readFoodTypeDTO =  await InsertAsync<MenuDTO, T>(menuDTO, cancellationToken);

            return readFoodTypeDTO;
        }

        public async Task<T> UpdateAsync<T>(int id, MenuDTO menuDTO,
            CancellationToken cancellationToken)
        {
            await EnsureEmployeeValidOrThrowAsync(menuDTO, cancellationToken);

            T readFoodTypeDTO = await UpdateAsync<MenuDTO, T>(id, menuDTO, cancellationToken);

            return readFoodTypeDTO;
        }

        private async Task EnsureEmployeeValidOrThrowAsync(MenuDTO menuDTO,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            await EnsureEmployeeExistsOrThrowAsync(subjectId, cancellationToken);

            await EnsureEmployeeWorksAtRestaurantOrThrowAsync(subjectId,
                menuDTO.RestaurantId, cancellationToken);
        }

        private async Task EnsureEmployeeExistsOrThrowAsync(int subjectId,
            CancellationToken cancellationToken)
        {
            bool isEmployeeExisting = await _employeeRepository
                .IsExistingAsync(subjectId, cancellationToken);

            if (!isEmployeeExisting)
            {
                throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.EmployeeAuthorizationExceptionMessage);
            }
        }

        private async Task EnsureEmployeeWorksAtRestaurantOrThrowAsync(int subjectId,
            int restaurantId, CancellationToken cancellationToken)
        {
            bool isEmployeeWorkAtRestaurant = await _restaurantRepository
                .WorksAtRestaurant(subjectId, restaurantId, cancellationToken);

            if (!isEmployeeWorkAtRestaurant)
            {
                throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                ExceptionMessages.NotRestaurantEmployeeAuthorizationExceptionMessage);
            }
        }
    }
}
