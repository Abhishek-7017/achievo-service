using System.Threading.Tasks;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.Models.Models;
using Achievo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AchievoAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        private IAuthService _authService;

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await _authService.RegisterUserAsync(request);

            if (user is null)
            {
                return BadRequest("User Already Exists..");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] UserDto request)
        {
            var response = await _authService.LoginUserAsync(request);

            if (response is null)
            {
                return BadRequest("User Name or Password is incorrect");
            }

            return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshTokensAsync(request);

            if (response is null || response.AccessToken is null || response.RefreshToken is null)
            {
                return BadRequest("Invalid RefreshToken...");
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticateOnlyEndPoint()
        {
            return Ok("you are authenticated1");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminAuth()
        {
            return Ok("you are a Admin");
        }
    }
}
