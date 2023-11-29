using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class UpdateMenuHandler : IRequestHandler<UpdateMenuCommand, ReadMenuDTO>
    {
        private readonly ISqlMenuRepository _sqlMenuRepository;
        private readonly IMapper _mapper;

        public UpdateMenuHandler(ISqlMenuRepository sqlClientRepository,
            IMapper mapper)
        {
            _sqlMenuRepository = sqlClientRepository;
            _mapper = mapper;
        }

        public async Task<ReadMenuDTO> Handle(UpdateMenuCommand request,
            CancellationToken cancellationToken)
        {
            var menu = await _sqlMenuRepository
                .GetByIdAsync<Domain.Entities.Menu>(request.Id, cancellationToken);

            if (menu is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Menu));
            }

            menu = _mapper.Map<Domain.Entities.Menu>(request);

            ReadMenuDTO readMenuDTO = await _sqlMenuRepository
                .UpdateAsync<ReadMenuDTO>(menu, cancellationToken);

            return readMenuDTO;
        }
    }
}
