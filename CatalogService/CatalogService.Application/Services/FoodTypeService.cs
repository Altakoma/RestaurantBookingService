using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;

namespace CatalogService.Application.Services
{
    public class FoodTypeService : IFoodTypeService
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly IMapper _mapper;


        public FoodTypeService(IFoodTypeRepository foodTypeRepository,
            IMapper mapper)
        {
            _foodTypeRepository = foodTypeRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO? readFoodTypeDTO =
                await _foodTypeRepository.GetByIdAsync(id, cancellationToken);

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

        public async Task<ICollection<ReadFoodTypeDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadFoodTypeDTO> readFoodTypeDTOs =
                await _foodTypeRepository.GetAllAsync(cancellationToken);

            return readFoodTypeDTOs;
        }

        public async Task<ReadFoodTypeDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO? readFoodTypeDTO =
                await _foodTypeRepository.GetByIdAsync(id, cancellationToken);

            if (readFoodTypeDTO is null)
            {
                throw new NotFoundException(nameof(FoodType),
                    id.ToString(), typeof(FoodType));
            }

            return readFoodTypeDTO;
        }

        public async Task<ReadFoodTypeDTO> InsertAsync(FoodTypeDTO item,
            CancellationToken cancellationToken)
        {
            var foodType = _mapper.Map<FoodType>(item);

            foodType = await _foodTypeRepository
                             .InsertAsync(foodType, cancellationToken);

            bool isInserted = await _foodTypeRepository
                                    .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    foodType.Id.ToString(), typeof(FoodType));
            }

            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            return readFoodTypeDTO;
        }

        public async Task<ReadFoodTypeDTO> UpdateAsync(int id,
            FoodTypeDTO item, CancellationToken cancellationToken)
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

            ReadFoodTypeDTO? readFoodType =
                await _foodTypeRepository.GetByIdAsync(id, cancellationToken);

            if (readFoodType is null)
            {
                throw new NotFoundException(nameof(FoodType), id.ToString(),
                    typeof(FoodType));
            }

            return readFoodType;
        }
    }
}
