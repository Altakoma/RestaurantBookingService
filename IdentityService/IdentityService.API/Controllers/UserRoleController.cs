using IdentityService.BusinessLogic.DTOs.Exception;
using IdentityService.BusinessLogic.DTOs.UserRole;
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadUserRoleDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetUserRoleByIdAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadUserRoleDTO readUserRoleDTO = await _userRoleService
                                        .GetByIdAsync(id, cancellationToken);

            return Ok(readUserRoleDTO);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadUserRoleDTO>))]
        public async Task<IActionResult> GetAllUserRolesAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadUserRoleDTO> readUserRoleDTOs = await _userRoleService
                                         .GetAllAsync(cancellationToken);

            return Ok(readUserRoleDTOs);
        }
    }
}
