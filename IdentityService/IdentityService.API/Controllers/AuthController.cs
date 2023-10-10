using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IUserService userService, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(InsertUserDTO insertUserDTO)
        {
            var readUserDTO = await _userService.InsertAsync(insertUserDTO);

            return CreatedAtRoute(nameof(UserController.GetUserByIdAsync),
                new { id = readUserDTO.Id }, readUserDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync([FromBody] LoginDTO loginDTO)
        {
            var tokensDTO = await _userService.GetUserAsync(loginDTO.Login, loginDTO.Password);

            return Ok(tokensDTO);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var generatedTokenDTO = await _refreshTokenService.VerifyAndGenerateTokenAsync();

            return Ok(generatedTokenDTO);
        }
    }
}
