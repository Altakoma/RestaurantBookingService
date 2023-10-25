using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services;

namespace BookingService.Application.Services
{
    public class TableService : BaseService<Table>, ITableService
    {
        public TableService(ITableRepository tableRepository,
            IMapper mapper) : base(tableRepository, mapper)
        {
        }
    }
}
