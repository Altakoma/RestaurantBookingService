using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMenuService _menuService;
        private readonly IEmployeeService _employeeService;

        public RestaurantController(IRestaurantService restaurantService,
            IMenuService menuService, IEmployeeService employeeService)
        {
            _restaurantService = restaurantService;
            _menuService = menuService;
            _employeeService = employeeService;
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

            return CreatedAtAction(nameof(GetRestaurant), new { readRestaurantDTO.Id },
                readRestaurantDTO);
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

        [HttpGet("{id}/menu")]
        public async Task<IActionResult> GetMenu(int id)
        {
            ICollection<ReadMenuDTO> readMenuDTOs =
                await _menuService.GetAllByRestaurantIdAsync(id);

            return Ok(readMenuDTOs);
        }

        [HttpGet("{id}/employee")]
        public async Task<IActionResult> GetEmployees(int id)
        {
            ICollection<ReadEmployeeDTO> readEmployeeDTOs =
                await _employeeService.GetAllByRestaurantIdAsync(id);

            return Ok(readEmployeeDTOs);
        }
    }
}
