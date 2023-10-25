using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.DTOs.Exception;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IBaseRestaurantService _restaurantService;
        private readonly IMenuService _menuService;
        private readonly IBaseEmployeeService _employeeService;

        public RestaurantController(IBaseRestaurantService restaurantService,
            IMenuService menuService, IBaseEmployeeService employeeService)
        {
            _restaurantService = restaurantService;
            _menuService = menuService;
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadRestaurantDTO>))]
        public async Task<IActionResult> GetAllRestaurantsAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadRestaurantDTO> readRestaurantDTOs =
                await _restaurantService.GetAllAsync<ReadRestaurantDTO>(cancellationToken);

            return Ok(readRestaurantDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetRestaurantAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO readRestaurantDTO =
                await _restaurantService.GetByIdAsync<ReadRestaurantDTO>(id, cancellationToken);

            return Ok(readRestaurantDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertRestaurantAsync(
            [FromBody] InsertRestaurantDTO insertRestaurantDTO,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .InsertAsync<InsertRestaurantDTO, ReadRestaurantDTO>(insertRestaurantDTO,
                cancellationToken);

            return CreatedAtAction(nameof(GetRestaurantAsync),
                                   new { id = readRestaurantDTO.Id }, readRestaurantDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRestaurantAsync([FromRoute] int id,
            [FromBody] UpdateRestaurantDTO updateRestaurantDTO,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .UpdateAsync<UpdateRestaurantDTO, ReadRestaurantDTO>(id,
                updateRestaurantDTO, cancellationToken);

            return Ok(readRestaurantDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRestaurantAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _restaurantService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

        [HttpGet("{id}/menu")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadMenuDTO>))]
        public async Task<IActionResult> GetMenuAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ICollection<ReadMenuDTO> readMenuDTOs =
                await _menuService.GetAllByRestaurantIdAsync<ReadMenuDTO>(id, cancellationToken);

            return Ok(readMenuDTOs);
        }

        [HttpGet("{id}/employee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadEmployeeDTO>))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeesAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ICollection<ReadEmployeeDTO> readEmployeeDTOs = await _employeeService
                .GetAllByRestaurantIdAsync<ReadEmployeeDTO>(id, cancellationToken);

            return Ok(readEmployeeDTOs);
        }
    }
}
