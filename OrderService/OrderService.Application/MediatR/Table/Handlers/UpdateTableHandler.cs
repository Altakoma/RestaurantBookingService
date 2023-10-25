using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, ReadTableDTO>
    {
        private readonly IWriteTableRepository _writeTableRepository;
        private readonly IReadTableRepository _readTableRepository;
        private readonly IMapper _mapper;

        public UpdateTableHandler(IWriteTableRepository writeTableRepository,
            IReadTableRepository readTableRepository, IMapper mapper)
        {
            _writeTableRepository = writeTableRepository;
            _readTableRepository = readTableRepository;
            _mapper = mapper;
        }

        public async Task<ReadTableDTO> Handle(UpdateTableCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Table table = 
                await _readTableRepository.GetByIdAsync(request.Id, cancellationToken);

            if (table is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Table),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }

            table = _mapper.Map<Domain.Entities.Table>(request);

            _writeTableRepository.Update(table);

            bool isUpdated = await _writeTableRepository.
                                   SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateTableHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }

            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            return readTableDTO;
        }
    }
}
