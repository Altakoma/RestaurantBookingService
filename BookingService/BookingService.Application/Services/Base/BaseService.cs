using AutoMapper;
using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services.Base
{
    public class BaseService<K> : IBaseService where K : BaseEntity
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
            T readItemDTO = await _repository.GetByIdAsync<T>(id, cancellationToken);

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

            T readItemDTO = await _repository.InsertAsync<T>(item, cancellationToken);

            bool isInserted = await _repository.SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    item?.ToString() ?? string.Empty, typeof(K));
            }

            return readItemDTO;
        }

        public async Task<T> UpdateAsync<U, T>(int id, U updateItemDTO,
            CancellationToken cancellationToken)
        {
            var item = _mapper.Map<K>(updateItemDTO);
            item.Id = id;

            T itemDTO = await _repository.UpdateAsync<T>(item, cancellationToken);

            bool isUpdated = await _repository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync),
                    id.ToString(), typeof(K));
            }

            return itemDTO;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _repository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync),
                    id.ToString(), typeof(K));
            }
        }
    }
}
