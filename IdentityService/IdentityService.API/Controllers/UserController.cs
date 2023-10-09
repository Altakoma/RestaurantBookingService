using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.BusinessLogic.Services.Interfaces;
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

        [HttpGet("{id}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var readUserDTO = await _userService.GetByIdAsync(id);

            return Ok(readUserDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var readUserDTOs = await _userService.GetAllAsync();

            return Ok(readUserDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var readUserDTO = await _userService.UpdateAsync(id, updateUserDTO);

            return Ok(readUserDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);

            return NoContent();
        }
    }
}
