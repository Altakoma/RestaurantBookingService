using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class InsertMenuHandler : IRequestHandler<InsertMenuCommand, ReadMenuDTO>
    {
        private readonly IWriteMenuRepository _writeMenuRepository;
        private readonly IReadMenuRepository _readMenuRepository;
        private readonly IMapper _mapper;

        public InsertMenuHandler(IWriteMenuRepository writeMenuRepository,
            IReadMenuRepository readMenuRepository, IMapper mapper)
        {
            _writeMenuRepository = writeMenuRepository;
            _readMenuRepository = readMenuRepository;
            _mapper = mapper;
        }

        public async Task<ReadMenuDTO> Handle(InsertMenuCommand request,
            CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Domain.Entities.Menu>(request);

            await _writeMenuRepository.InsertAsync(menu, cancellationToken);

            bool isInserted = await _writeMenuRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertMenuHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }

            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            return readMenuDTO;
        }
    }
}
