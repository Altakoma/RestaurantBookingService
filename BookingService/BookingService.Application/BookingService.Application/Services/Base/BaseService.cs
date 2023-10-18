using AutoMapper;
using BookingService.Application.RepositoryInterfaces.Base;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services.Base
{
    public class BaseService<T> : IBaseService<T>
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken)
        {
            ICollection<U> items = await _repository.GetAllAsync<U>(cancellationToken);

            return items;
        }

        public async Task<U> InsertAsync<U>(T item, CancellationToken cancellationToken)
        {
            item = await _repository.InsertAsync(item, cancellationToken);

            U itemDTO = _mapper.Map<U>(item);

            return itemDTO;
        }
    }
}
