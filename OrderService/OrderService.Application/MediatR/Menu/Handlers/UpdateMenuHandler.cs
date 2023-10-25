using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class UpdateMenuHandler : IRequestHandler<UpdateMenuCommand, ReadMenuDTO>
    {
        private readonly IWriteMenuRepository _writeMenuRepository;
        private readonly IReadMenuRepository _readMenuRepository;
        private readonly IMapper _mapper;

        public UpdateMenuHandler(IWriteMenuRepository writeMenuRepository,
            IReadMenuRepository readMenuRepository, IMapper mapper)
        {
            _writeMenuRepository = writeMenuRepository;
            _readMenuRepository = readMenuRepository;
            _mapper = mapper;
        }

        public async Task<ReadMenuDTO> Handle(UpdateMenuCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Menu menu = await _readMenuRepository.GetByIdAsync(request.Id, cancellationToken);

            if (menu is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Menu),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }

            menu = _mapper.Map<Domain.Entities.Menu>(request);

            _writeMenuRepository.Update(menu);

            bool isUpdated = await _writeMenuRepository.
                                   SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateMenuHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }

            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            return readMenuDTO;
        }
    }
}
