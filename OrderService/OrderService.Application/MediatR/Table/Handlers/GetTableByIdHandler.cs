using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Table.Queries;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class GetTableByIdHandler : IRequestHandler<GetTableByIdQuery, ReadTableDTO>
    {
        private readonly IReadTableRepository _readTableRepository;
        private readonly IMapper _mapper;

        public GetTableByIdHandler(IReadTableRepository readTableRepository,
            IMapper mapper)
        {
            _readTableRepository = readTableRepository;
            _mapper = mapper;
        }

        public async Task<ReadTableDTO> Handle(GetTableByIdQuery request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Table table = await _readTableRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (table is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Table),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }

            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            return readTableDTO;
        }
    }
}
