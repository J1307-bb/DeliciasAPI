using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsers()
        {
            var result = await _userService.ObtenerUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var result = await _userService.ObtenerUser(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UserResponse request)
        {
            var result = await _userService.CrearUser(request);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] UserResponse request, int id)
        {
            var result = await _userService.ActualizarUser(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _userService.EliminarUser(id);
            return Ok(result);
        }
    }
}
