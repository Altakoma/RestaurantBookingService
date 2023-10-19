using CatalogService.Application.DTOs.Exception;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadMenuDTO>))]
        public async Task<IActionResult> GetAllFoodAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadMenuDTO> menuDTOs =
                await _menuService.GetAllAsync<ReadMenuDTO>(cancellationToken);

            return Ok(menuDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadMenuDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetFoodAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO menuDTO =
                await _menuService.GetByIdAsync<ReadMenuDTO>(id, cancellationToken);

            return Ok(menuDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadMenuDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertFoodAsync(
            [FromBody] InsertMenuDTO insertMenuDTO,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO readMenuDTO = await _menuService
                .InsertAsync<ReadMenuDTO>(insertMenuDTO, cancellationToken);

            return CreatedAtAction(nameof(GetFoodAsync),
                                   new { id = readMenuDTO.Id }, readMenuDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadMenuDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateFoodAsync([FromRoute] int id,
            [FromBody] UpdateMenuDTO updateMenuDTO,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO readMenuDTO = await _menuService
                .UpdateAsync<ReadMenuDTO>(id, updateMenuDTO, cancellationToken);

            return Ok(readMenuDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteFoodAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _menuService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
