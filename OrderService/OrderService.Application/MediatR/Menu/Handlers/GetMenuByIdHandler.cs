using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Menu.Queries;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class GetMenuByIdHandler : IRequestHandler<GetMenuByIdQuery, ReadMenuDTO>
    {
        private readonly IReadMenuRepository _readMenuRepository;
        private readonly IMapper _mapper;

        public GetMenuByIdHandler(IReadMenuRepository readMenuRepository,
            IMapper mapper)
        {
            _readMenuRepository = readMenuRepository;
            _mapper = mapper;
        }

        public async Task<ReadMenuDTO> Handle(GetMenuByIdQuery request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Menu menu = await _readMenuRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (menu is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Menu),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }

            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            return readMenuDTO;
        }
    }
}
