using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Menu.Queries;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class GetAllMenuHandler : IRequestHandler<GetAllMenuQuery, ICollection<ReadMenuDTO>>
    {
        private readonly IReadMenuRepository _readMenuRepository;
        private readonly IMapper _mapper;

        public GetAllMenuHandler(IReadMenuRepository readMenuRepository,
            IMapper mapper)
        {
            _readMenuRepository = readMenuRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReadMenuDTO>> Handle(GetAllMenuQuery request,
            CancellationToken cancellationToken)
        {
            ICollection<Domain.Entities.Menu> menu =
                await _readMenuRepository.GetAllAsync(cancellationToken);

            var readMenuDTOs = _mapper.Map<ICollection<ReadMenuDTO>>(menu);

            return readMenuDTOs;
        }
    }
}
