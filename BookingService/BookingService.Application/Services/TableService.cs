using AutoMapper;
using BookingService.Application.DTOs.Table;
using BookingService.Application.Interfaces.GrpcServices;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.Services.Base;
using BookingService.Application.TokenParsers.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BookingService.Application.Services
{
    public class TableService : BaseService<Table>, ITableService
    {
        private readonly ITokenParser _tokenParser;
        private readonly ITableRepository _tableRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGrpcClientEmployeeService _grpcEmployeeClientService;

        public TableService(ITableRepository tableRepository,
            IMapper mapper, ITokenParser tokenParser,
            IHttpContextAccessor httpContextAccessor,
            IGrpcClientEmployeeService grpcEmployeeClientService)
            : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
            _tokenParser = tokenParser;
            _httpContextAccessor = httpContextAccessor;
            _grpcEmployeeClientService = grpcEmployeeClientService;
        }

        public override async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await EmployeeWorksAtRestaurantByTableId(id, cancellationToken);

            await base.DeleteAsync(id, cancellationToken);
        }

        public async Task<T> InsertAsync<T>(InsertTableDTO insertItemDTO,
            CancellationToken cancellationToken)
        {
            await EmployeeWorksAtRestaurant(insertItemDTO.RestaurantId, cancellationToken);

            return await base.InsertAsync<InsertTableDTO, T>(insertItemDTO, cancellationToken);
        }

        public async override Task<T> UpdateAsync<U, T>(int id, U updateItemDTO,
            CancellationToken cancellationToken)
        {
            await EmployeeWorksAtRestaurantByTableId(id, cancellationToken);

            return await base.UpdateAsync<U, T>(id, updateItemDTO, cancellationToken);
        }

        private async Task EmployeeWorksAtRestaurantByTableId(int tableId,
            CancellationToken cancellationToken)
        {
            int restaurantId = await _tableRepository
                .GetRestaurantIdByTableIdAsync(tableId, cancellationToken);

            await EmployeeWorksAtRestaurant(restaurantId, cancellationToken);
        }

        private async Task EmployeeWorksAtRestaurant(int restaurantId,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser.ParseSubjectId(
                                        _httpContextAccessor?.HttpContext?.Request.Headers);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = subjectId,
                RestaurantId = restaurantId,
            };

            IsWorkingAtRestaurantReply reply = await _grpcEmployeeClientService
                .EmployeeWorksAtRestaurant(request, cancellationToken);

            if (!reply.IsEmployeeWorkingAtRestaurant)
            {
                throw new AuthorizationException(subjectId.ToString(),
                    ExceptionMessages.EmployeeAuthorizationExceptionMessage);
            }
        }
    }
}
