﻿using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Application.TokenParsers.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Services
{
    public class FoodTypeService : BaseService<FoodType>, IBaseFoodTypeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenParser _tokenParser;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IFoodTypeCacheAccessor _foodTypeCacheAccessor;

        public FoodTypeService(IFoodTypeRepository foodTypeRepository,
            IMapper mapper, IHttpContextAccessor httpContextAccessor,
            ITokenParser tokenParser, IEmployeeRepository employeeRepository,
            IFoodTypeCacheAccessor foodTypeCacheAccessor)
            : base(foodTypeRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenParser = tokenParser;

            _employeeRepository = employeeRepository;

            _foodTypeCacheAccessor = foodTypeCacheAccessor;
        }

        public override async Task<T> GetByIdAsync<T>(int id,
            CancellationToken cancellationToken)
        {
            T itemDTO = await _foodTypeCacheAccessor
                .GetByResourceIdAsync<T>(id.ToString(), cancellationToken);

            return itemDTO;
        }

        public override async Task<T> InsertAsync<U, T>(U foodTypeDTO, CancellationToken cancellationToken)
        {
            await EnsureTokenValidOrThrowAsync(cancellationToken);

            return await base.InsertAsync<U, T>(foodTypeDTO, cancellationToken);
        }

        public override async Task<T> UpdateAsync<U, T>(int id, U foodTypeDTO,
            CancellationToken cancellationToken)
        {
            await EnsureTokenValidOrThrowAsync(cancellationToken);

            await _foodTypeCacheAccessor.DeleteResourceByIdAsync(id.ToString(),
                                                                 cancellationToken);

            return await base.UpdateAsync<U, T>(id, foodTypeDTO, cancellationToken);
        }

        public override async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await EnsureTokenValidOrThrowAsync(cancellationToken);

            await _foodTypeCacheAccessor.DeleteResourceByIdAsync(id.ToString(),
                                                                 cancellationToken);

            return await base.DeleteAsync(id, cancellationToken);
        }

        private async Task EnsureTokenValidOrThrowAsync(CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            bool isExist = await _employeeRepository.ExistsAsync(subjectId, cancellationToken);

            if (!isExist)
            {
                throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.EmployeeAuthorizationExceptionMessage);
            }
        }
    }
}
