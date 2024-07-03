using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly string _secretKey;
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService, IConfiguration config)
        {
            _loginService = loginService;
            _secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
        }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginResponse request)
    {
        var result = await _loginService.Authenticate(request, _secretKey);

        if (result == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(result);
    }
    }
}
