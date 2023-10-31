using AutoMapper;
using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class InsertMenuHandler : IRequestHandler<InsertMenuCommand, ReadMenuDTO>
    {
        private readonly ISqlMenuRepository _sqlMenuRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;

        public InsertMenuHandler(ISqlMenuRepository sqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper)
        {
            _sqlMenuRepository = sqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _mapper = mapper;
        }

        public async Task<ReadMenuDTO> Handle(InsertMenuCommand request,
            CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Domain.Entities.Menu>(request);

            ReadMenuDTO readMenuDTO = await _sqlMenuRepository
                .InsertAsync<ReadMenuDTO>(menu, cancellationToken);

            bool isInserted = await _sqlMenuRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertMenuHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }

            return readMenuDTO;
        }
    }
}
