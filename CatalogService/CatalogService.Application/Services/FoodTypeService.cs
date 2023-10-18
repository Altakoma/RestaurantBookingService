using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class FoodTypeService : BaseService<FoodType>, IFoodTypeService
    {
        private readonly IFoodTypeRepository _foodTypeRepository;

        public FoodTypeService(IFoodTypeRepository foodTypeRepository,
            IMapper mapper) : base(foodTypeRepository, mapper)
        {
            _foodTypeRepository = foodTypeRepository;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO? readFoodTypeDTO =
                await _foodTypeRepository.GetByIdAsync<ReadFoodTypeDTO>(id, cancellationToken);

            if (readFoodTypeDTO is null)
            {
                throw new NotFoundException(nameof(FoodType), id.ToString(),
                    typeof(FoodType));
            }

            await _foodTypeRepository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _foodTypeRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(FoodType));
            }
        }

        public async Task<T> GetByIdAsync<T>(int id, CancellationToken cancellationToken)
        {
            T? readFoodTypeDTO = await _foodTypeRepository
                .GetByIdAsync<T>(id, cancellationToken);

            if (readFoodTypeDTO is null)
            {
                throw new NotFoundException(nameof(FoodType),
                    id.ToString(), typeof(FoodType));
            }

            return readFoodTypeDTO;
        }

        public async Task<T> UpdateAsync<U, T>(int id, U item,
            CancellationToken cancellationToken)
        {
            var foodType = _mapper.Map<FoodType>(item);
            foodType.Id = id;

            _foodTypeRepository.Update(foodType);

            bool isUpdated = await _foodTypeRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync),
                    id.ToString(), typeof(FoodType));
            }

            T? readFoodType = await _foodTypeRepository
                .GetByIdAsync<T>(id, cancellationToken);

            if (readFoodType is null)
            {
                throw new NotFoundException(nameof(FoodType), id.ToString(),
                    typeof(FoodType));
            }

            return readFoodType;
        }
    }
}
