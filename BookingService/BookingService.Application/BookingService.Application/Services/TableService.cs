using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    public class TableService : BaseService<Table>, IBaseService
    {
        public TableService(ITableRepository tableRepository,
            IMapper mapper) : base(tableRepository, mapper)
        {
        }
    }
}
