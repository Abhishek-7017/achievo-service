using System.Threading.Tasks;
using Achievo.Contracts.Dto;
using Achievo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AchievoAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        private IUserService _userService;

        [Authorize]
        [HttpGet("allUsers")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            List<UserDto> users = _userService.GetAllUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("userById")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            UserDto? user = await _userService.GetUserById(id);

            if (user is null)
            {
                return BadRequest("User Not Found");
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut("udateDetails")]
        public async Task<ActionResult<UserDto>> UpdateUserDetails(UserDto request)
        {
            UserDto? response = await _userService.UpdateUser(request);

            if (response is null)
            {
                return BadRequest("User not Found");
            }

            return Ok(response);
        }
    }
}
