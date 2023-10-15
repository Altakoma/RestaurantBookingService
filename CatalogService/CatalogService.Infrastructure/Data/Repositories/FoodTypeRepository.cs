using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class FoodTypeRepository : WriteRepository<FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(CatalogServiceDbContext dbContext,
            IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            FoodType? foodType = await _dbContext.FoodTypes
                .FirstOrDefaultAsync(foodType => foodType.Id == id);

            if (foodType is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(foodType);
        }

        public async Task<ICollection<ReadFoodTypeDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var readFoodTypeDTOs = await _mapper.ProjectTo<ReadFoodTypeDTO>(
                _dbContext.FoodTypes.Select(foodType => foodType))
                .ToListAsync();

            return readFoodTypeDTOs;
        }

        public async Task<ReadFoodTypeDTO?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var readFoodTypeDTO = await _mapper.ProjectTo<ReadFoodTypeDTO>(
                _dbContext.FoodTypes.Where(foodType => foodType.Id == id))
                .SingleOrDefaultAsync();

            return readFoodTypeDTO;
        }
    }
}
