using AutoMapper;
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

        public async Task<T> ExecuteAndCheckAsync<T>(Func<Task<T>> function,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            bool isExist = await _employeeRepository.Exists(subjectId, cancellationToken);

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
