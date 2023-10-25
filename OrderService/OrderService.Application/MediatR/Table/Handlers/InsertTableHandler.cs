using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class InsertTableHandler : IRequestHandler<InsertTableCommand, ReadTableDTO>
    {
        private readonly IWriteTableRepository _writeTableRepository;
        private readonly IReadTableRepository _readTableRepository;
        private readonly IMapper _mapper;

        public InsertTableHandler(IWriteTableRepository writeTableRepository,
            IReadTableRepository readTableRepository, IMapper mapper)
        {
            _writeTableRepository = writeTableRepository;
            _readTableRepository = readTableRepository;
            _mapper = mapper;
        }

        public async Task<ReadTableDTO> Handle(InsertTableCommand request,
            CancellationToken cancellationToken)
        {
            var table = _mapper.Map<Domain.Entities.Table>(request);

            await _writeTableRepository.InsertAsync(table, cancellationToken);

            bool isInserted = await _writeTableRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertTableHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }

            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            return readTableDTO;
        }
    }
}
