using DeliciasAPI.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneUser(int id)
        {
            var result = await _userService.GetOneUser(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> PostNewUser(UserDto userDto)
        {
            var result = await _userService.PostNewUser(userDto);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            var result = await _userService.UpdateUser(id, userDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
