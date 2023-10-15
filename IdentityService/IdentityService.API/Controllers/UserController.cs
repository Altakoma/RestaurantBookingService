using IdentityService.BusinessLogic.DTOs.Exception;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.DataAccess.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadUserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadUserDTO readUserDTO =
                await _userService.GetByIdAsync(id, cancellationToken);

            return Ok(readUserDTO);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadUserDTO>))]
        public async Task<IActionResult> GetAllUsersAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadUserDTO> readUserDTOs =
                await _userService.GetAllAsync(cancellationToken);

            return Ok(readUserDTOs);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadUserDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id,
            [FromBody] UpdateUserDTO updateUserDTO,
            CancellationToken cancellationToken)
        {
            ReadUserDTO readUserDTO =
                await _userService.UpdateAsync(id, updateUserDTO, cancellationToken);

            return Ok(readUserDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
