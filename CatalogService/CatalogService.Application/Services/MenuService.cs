﻿using AutoMapper;
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

        public async Task<T> InsertAsync<T>(InsertMenuDTO insertItemDTO,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            bool isExist = await _employeeRepository.Exists(subjectId, cancellationToken);

            if (isExist)
            {
                bool isWorkAtRestaurant = await _restaurantRepository.WorksAtRestaurant(subjectId,
                    insertItemDTO.RestaurantId, cancellationToken);

                if (isWorkAtRestaurant)
                {
                    return await InsertAsync<InsertMenuDTO, T>(insertItemDTO, cancellationToken);
                }
                else
                {
                    throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.NotRestaurantEmployeeAuthorizationExceptionMessage);
                }
            }
            else
            {
                throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.EmployeeAuthorizationExceptionMessage);
            }
        }

        public async Task<T> UpdateAsync<T>(int id, UpdateMenuDTO updateItemDTO,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            bool isExist = await _employeeRepository.Exists(subjectId, cancellationToken);

            if (isExist)
            {
                bool isWorkAtRestaurant = await _restaurantRepository.WorksAtRestaurant(subjectId,
                    updateItemDTO.RestaurantId, cancellationToken);

                if (isWorkAtRestaurant)
                {
                    return await UpdateAsync<UpdateMenuDTO, T>(id, updateItemDTO, cancellationToken);
                }
                else
                {
                    throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.NotRestaurantEmployeeAuthorizationExceptionMessage);
                }
            }
            else
            {
                throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.EmployeeAuthorizationExceptionMessage);
            }
        }

        public new async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            bool isExist = await _employeeRepository.Exists(subjectId, cancellationToken);

            if (isExist)
            {
                Menu? menu = await _menuRepository.GetByIdAsync<Menu>(id,cancellationToken);

                if (menu is null)
                {
                    throw new NotFoundException(nameof(Menu), id.ToString(), typeof(Menu));
                }

                bool isWorkAtRestaurant = await _restaurantRepository.WorksAtRestaurant(subjectId,
                    menu.RestaurantId, cancellationToken);

                if (isWorkAtRestaurant)
                {
                    await base.DeleteAsync(id, cancellationToken);
                }
                else
                {
                    throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.NotRestaurantEmployeeAuthorizationExceptionMessage);
                }
            }
            else
            {
                throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.EmployeeAuthorizationExceptionMessage);
            }
        }
    }
}
