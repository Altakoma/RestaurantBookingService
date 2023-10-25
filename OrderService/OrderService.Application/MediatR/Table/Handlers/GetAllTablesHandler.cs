using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Table.Queries;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class GetAllTablesHandler : IRequestHandler<GetAllTablesQuery, ICollection<ReadTableDTO>>
    {
        private readonly IReadTableRepository _readTableRepository;
        private readonly IMapper _mapper;

        public GetAllTablesHandler(IReadTableRepository readTableRepository,
            IMapper mapper)
        {
            _readTableRepository = readTableRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReadTableDTO>> Handle(GetAllTablesQuery request,
            CancellationToken cancellationToken)
        {
            ICollection<Domain.Entities.Table> tables =
                await _readTableRepository.GetAllAsync(cancellationToken);

            var readTableDTOs = _mapper.Map<ICollection<ReadTableDTO>>(tables);

            return readTableDTOs;
        }
    }
}
