using AutoMapper;
using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services.Base;

namespace CatalogService.Application.Services.Base
{
    public abstract class BaseService<K> : IBaseService where K : BaseEntity
    {
        private readonly IRepository<K> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IRepository<K> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<T> GetByIdAsync<T>(int id, CancellationToken cancellationToken)
        {
            T? readItemDTO = await _repository.GetByIdAsync<T>(id, cancellationToken);

            if (readItemDTO is null)
            {
                throw new NotFoundException(nameof(FoodType),
                    id.ToString(), typeof(FoodType));
            }

            return readItemDTO;
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

        public async Task<T> UpdateAsync<U, T>(int id, U updateItemDTO,
            CancellationToken cancellationToken)
        {
            var item = _mapper.Map<K>(updateItemDTO);
            item.Id = id;

            _repository.Update(item);

            bool isUpdated = await _repository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync),
                    id.ToString(), typeof(K));
            }

            T? readItemDTO = await _repository
                                   .GetByIdAsync<T>(id, cancellationToken);

            if (readItemDTO is null)
            {
                throw new NotFoundException(nameof(K),
                    id.ToString(), typeof(K));
            }

            return readItemDTO;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            K? item = await _repository.GetByIdAsync<K>(id, cancellationToken);

            if (item is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            await _repository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _repository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync),
                    id.ToString(), typeof(Employee));
            }
        }
    }
}
