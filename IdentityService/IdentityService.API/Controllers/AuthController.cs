using IdentityService.BusinessLogic.DTOs.TokenDTOs;
using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Encodings.Web;

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
        public async Task<IActionResult> Register(InsertUserDTO insertUserDTO)
        {
            var readUserDTO = await _userService.InsertAsync(insertUserDTO);

            return CreatedAtRoute(nameof(UserController.GetUserById),
                new { id = readUserDTO.Id }, readUserDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(string login, string password)
        {
            var tokensDTO = await _userService.GetUserAsync(login, password);

            return Ok(tokensDTO);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var generatedTokenDTO = await _refreshTokenService.VerifyAndGenerateToken();

            return Ok(generatedTokenDTO);
        }
    }
}
