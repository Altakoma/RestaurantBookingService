using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.Services.Interfaces;
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
        public async Task<IActionResult> GetAllFoodAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadMenuDTO> menuDTOs =
                await _menuService.GetAllAsync(cancellationToken);

            return Ok(menuDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO menuDTO =
                await _menuService.GetByIdAsync(id, cancellationToken);

            return Ok(menuDTO);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFoodAsync(
            [FromBody] InsertMenuDTO insertMenuDTO,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO readMenuDTO =
                await _menuService.InsertAsync(insertMenuDTO, cancellationToken);

            return CreatedAtAction(nameof(GetFoodAsync),
                                   new { readMenuDTO.Id }, readMenuDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFoodAsync([FromRoute] int id,
            [FromBody] UpdateMenuDTO updateMenuDTO,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO readMenuDTO =
                await _menuService.UpdateAsync(id, updateMenuDTO, cancellationToken);

            return Ok(readMenuDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _menuService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
