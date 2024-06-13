using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginResponse request)
    {
        var result = await _loginService.Authenticate(request);

        if (result == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(result);
    }
    }
}
