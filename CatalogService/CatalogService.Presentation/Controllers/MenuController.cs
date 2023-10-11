using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFood()
        {
            ICollection<ReadMenuDTO> menuDTOs =
                await _menuService.GetAllAsync();

            return Ok(menuDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(int id)
        {
            ReadMenuDTO menuDTO = await _menuService.GetByIdAsync(id);

            return Ok(menuDTO);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFood(InsertMenuDTO insertMenuDTO)
        {
            ReadMenuDTO readMenuDTO = await _menuService
                .InsertAsync(insertMenuDTO);

            return CreatedAtAction(nameof(GetFood), readMenuDTO,
                readMenuDTO.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(int id,
            UpdateMenuDTO updateMenuDTO)
        {
            ReadMenuDTO readMenuDTO = await _menuService
                .UpdateAsync(id, updateMenuDTO);

            return Ok(readMenuDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            await _menuService.DeleteAsync(id);

            return NoContent();
        }
    }
}
