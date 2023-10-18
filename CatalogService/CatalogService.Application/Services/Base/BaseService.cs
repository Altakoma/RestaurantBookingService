using AutoMapper;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services.Base;

namespace CatalogService.Application.Services.Base
{
    public abstract class BaseService<K> : IBaseService
    {
        private readonly IRepository<K> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IRepository<K> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<T>> GetAllAsync<T>(CancellationToken cancellationToken)
        {
            ICollection<T> items = await _repository.GetAllAsync<T>(cancellationToken);

            return items;
        }

        public async Task<T> InsertAsync<U, T>(U insertItemDTO,
            CancellationToken cancellationToken)
        {
            var item = _mapper.Map<K>(insertItemDTO);

            item = await _repository.InsertAsync(item, cancellationToken);

            bool isInserted = await _repository.SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    item?.ToString() ?? string.Empty, typeof(K));
            }

            T readItemDTO = _mapper.Map<T>(item);

            return readItemDTO;
        }
    }
}
