using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodTypeController : ControllerBase
    {
        private readonly IFoodTypeService _foodTypeService;

        public FoodTypeController(IFoodTypeService foodTypeService)
        {
            _foodTypeService = foodTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFoodTypes()
        {
            ICollection<ReadFoodTypeDTO> readFoodTypeDTOs =
                await _foodTypeService.GetAllAsync();

            return Ok(readFoodTypeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            ReadFoodTypeDTO readFoodTypeDTO = await _foodTypeService.GetByIdAsync(id);

            return Ok(readFoodTypeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFoodType(FoodTypeDTO foodTypeDTO)
        {
            ReadFoodTypeDTO readFoodTypeDTO = await _foodTypeService
                .InsertAsync(foodTypeDTO);

            return CreatedAtAction(nameof(GetEmployee), new { readFoodTypeDTO.Id },
                readFoodTypeDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFoodType(int id,
            FoodTypeDTO foodTypeDTO)
        {
            ReadFoodTypeDTO readFoodTypeDTO =
                await _foodTypeService.UpdateAsync(id, foodTypeDTO);

            return Ok(readFoodTypeDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodType(int id)
        {
            await _foodTypeService.DeleteAsync(id);

            return NoContent();
        }
    }
}
