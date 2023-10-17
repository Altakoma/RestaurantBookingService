using IdentityService.BusinessLogic.DTOs.Exception;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
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

        public AuthController(IUserService userService,
            IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadUserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] InsertUserDTO insertUserDTO,
            CancellationToken cancellationToken)
        {
            ReadUserDTO readUserDTO = await _userService
                .InsertAsync(insertUserDTO, cancellationToken);

            return CreatedAtRoute(nameof(UserController.GetUserByIdAsync),
                new { id = readUserDTO.Id }, readUserDTO);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadUserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> LogInAsync(
            [FromBody] LoginDTO loginDTO,
            CancellationToken cancellationToken)
        {
            TokenDTO tokensDTO = await _userService.GetUserAsync(
                    loginDTO.Login, loginDTO.Password, cancellationToken);

            return Ok(tokensDTO);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> RefreshTokenAsync(
            CancellationToken cancellationToken)
        {
            TokenDTO generatedTokenDTO = await _refreshTokenService
                .VerifyAndGenerateTokenAsync(cancellationToken);

            return Ok(generatedTokenDTO);
        }
    }
}
