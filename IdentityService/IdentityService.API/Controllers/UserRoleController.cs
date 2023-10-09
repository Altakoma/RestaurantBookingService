using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet("{id}", Name = nameof(GetUserRoleById))]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            var readUserRoleDTO = await _userRoleService.GetByIdAsync(id);

            return Ok(readUserRoleDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserRoles()
        {
            var readUserRoleDTOs = await _userRoleService.GetAllAsync();

            return Ok(readUserRoleDTOs);
        }
    }
}
