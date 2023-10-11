using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            ICollection<ReadRestaurantDTO> readRestaurantDTOs =
                await _restaurantService.GetAllAsync();

            return Ok(readRestaurantDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .GetByIdAsync(id);

            return Ok(readRestaurantDTO);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRestaurant(
            InsertRestaurantDTO insertRestaurantDTO)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .InsertAsync(insertRestaurantDTO);

            return CreatedAtAction(nameof(GetRestaurant), readRestaurantDTO,
                readRestaurantDTO.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id,
            UpdateRestaurantDTO updateRestaurantDTO)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .UpdateAsync(id, updateRestaurantDTO);

            return Ok(readRestaurantDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _restaurantService.DeleteAsync(id);

            return NoContent();
        }
    }
}
