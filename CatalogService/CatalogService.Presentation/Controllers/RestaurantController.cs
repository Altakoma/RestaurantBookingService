﻿using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.DTOs.Exception;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadRestaurantDTO>))]
        public async Task<IActionResult> GetAllRestaurantsAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadRestaurantDTO> readRestaurantDTOs =
                await _restaurantService.GetAllAsync(cancellationToken);

            return Ok(readRestaurantDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetRestaurantAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO readRestaurantDTO =
                await _restaurantService.GetByIdAsync(id, cancellationToken);

            return Ok(readRestaurantDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertRestaurantAsync(
            [FromBody] InsertRestaurantDTO insertRestaurantDTO,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .InsertAsync(insertRestaurantDTO, cancellationToken);

            return CreatedAtAction(nameof(GetRestaurantAsync),
                                   new { readRestaurantDTO.Id }, readRestaurantDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateRestaurantAsync([FromRoute] int id,
            [FromBody] UpdateRestaurantDTO updateRestaurantDTO,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .UpdateAsync(id, updateRestaurantDTO, cancellationToken);

            return Ok(readRestaurantDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
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
                await _menuService.GetAllByRestaurantIdAsync(id, cancellationToken);

            return Ok(readMenuDTOs);
        }

        [HttpGet("{id}/employee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadEmployeeDTO>))]
        public async Task<IActionResult> GetEmployeesAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ICollection<ReadEmployeeDTO> readEmployeeDTOs = await _employeeService
                .GetAllByRestaurantIdAsync(id, cancellationToken);

            return Ok(readEmployeeDTOs);
        }
    }
}
