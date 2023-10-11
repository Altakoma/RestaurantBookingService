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

        public async Task DeleteAsync(int id)
        {
            FoodType? foodType = await _foodTypeRepository.GetByIdAsync(id);

            if (foodType is null)
            {
                throw new NotFoundException(id.ToString(), typeof(FoodType));
            }

            bool isDeleted = await _foodTypeRepository.DeleteAsync(foodType);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(FoodType));
            }
        }

        public async Task<ICollection<ReadFoodTypeDTO>> GetAllAsync()
        {
            ICollection<FoodType> foodTypes = await _foodTypeRepository.GetAllAsync();

            var foodTypeDTOs = _mapper.Map<ICollection<ReadFoodTypeDTO>>(foodTypes);

            return foodTypeDTOs;
        }

        public async Task<ReadFoodTypeDTO> GetByIdAsync(int id)
        {
            FoodType? foodType = await _foodTypeRepository.GetByIdAsync(id);

            if (foodType is null)
            {
                throw new NotFoundException(id.ToString(), typeof(FoodType));
            }

            var foodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            return foodTypeDTO;
        }

        public async Task<ReadFoodTypeDTO> InsertAsync(FoodTypeDTO item)
        {
            var foodType = _mapper.Map<FoodType>(item);

            (foodType, bool isInserted) = await _foodTypeRepository
                .InsertAsync(foodType);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    foodType.Id.ToString(), typeof(FoodType));
            }

            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            return readFoodTypeDTO;
        }

        public async Task<ReadFoodTypeDTO> UpdateAsync(int id, FoodTypeDTO item)
        {
            var foodType = _mapper.Map<FoodType>(item);

            bool isUpdated = await _foodTypeRepository.UpdateAsync(foodType);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync), id.ToString(),
                    typeof(FoodType));
            }

            foodType = await _foodTypeRepository.GetByIdAsync(id);

            var readFoodType = _mapper.Map<ReadFoodTypeDTO>(foodType);

            return readFoodType;
        }
    }
}
