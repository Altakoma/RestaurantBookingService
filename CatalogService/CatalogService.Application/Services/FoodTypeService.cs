using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.Interfaces.Repositories;
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

        public FoodTypeService(IFoodTypeRepository foodTypeRepository,
            IMapper mapper, IHttpContextAccessor httpContextAccessor,
            ITokenParser tokenParser, IEmployeeRepository employeeRepository)
            : base(foodTypeRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenParser = tokenParser;
            _employeeRepository = employeeRepository;
        }

        public new async Task<T> InsertAsync<U, T>(U foodTypeDTO, CancellationToken cancellationToken)
        {
            var insertAsync = async () =>
            {
                return await base.InsertAsync<U, T>(foodTypeDTO, cancellationToken);
            };

            T readFoodTypeDTO = await ExecuteAndCheckAsync<T>(insertAsync, cancellationToken);

            return readFoodTypeDTO;
        }

        public new async Task<T> UpdateAsync<U, T>(int id, U foodTypeDTO, 
            CancellationToken cancellationToken)
        {
            var updateAsync = async () =>
            {
                return await UpdateAsync<U, T>(id, foodTypeDTO, cancellationToken);
            };

            T readFoodTypeDTO = await ExecuteAndCheckAsync(updateAsync, cancellationToken);

            return readFoodTypeDTO;
        }

        public async Task<T> ExecuteAndCheckAsync<T>(Func<Task<T>> function,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            bool isExist = await _employeeRepository.ExistsAsync(subjectId, cancellationToken);

            if (isExist)
            {
                return await function();
            }
            else
            {
                throw new AuthorizationException(subjectId.ToString(), typeof(Employee),
                    ExceptionMessages.EmployeeAuthorizationExceptionMessage);
            }
        }
    }
}
