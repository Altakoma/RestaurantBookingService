using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, ReadTableDTO>
    {
        private readonly ISqlTableRepository _sqlTableRepository;
        private readonly IMapper _mapper;

        public UpdateTableHandler(ISqlTableRepository sqlTableRepository,
            IMapper mapper)
        {
            _sqlTableRepository = sqlTableRepository;
            _mapper = mapper;
        }

        public async Task<ReadTableDTO> Handle(UpdateTableCommand request,
            CancellationToken cancellationToken)
        {
            var table = await _sqlTableRepository
                .GetByIdAsync<Domain.Entities.Table>(request.Id, cancellationToken);

            if (table is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Table));
            }

            table = _mapper.Map<Domain.Entities.Table>(request);

            ReadTableDTO readTableDTO = await _sqlTableRepository
                .UpdateAsync<ReadTableDTO>(table, cancellationToken);

            return readTableDTO;
        }
    }
}
