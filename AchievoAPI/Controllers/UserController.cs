using System.Threading.Tasks;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.Models.Models;
using Achievo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AchievoAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        private IUserService _userService;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDetailsDto?>> GetUserByUserName(string userName)
        {
            UserDetailsDto? user = await _userService.GetUserByUserName(userName);

            if (user is null)
            {
                return BadRequest("User Not Found");
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUserDetails(UserDetailsDto request)
        {
            UserDetailsDto? response = await _userService.UpdateUser(request);

            if (response is null)
            {
                return BadRequest("User not Found");
            }

            return Ok(response);
        }
    }
}
