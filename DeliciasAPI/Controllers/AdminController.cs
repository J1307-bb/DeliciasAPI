using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _adminService.GetAdmins();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _adminService.GetAdmin(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdminResponse request)
        {
            var result = await _adminService.CreateAdmin(request);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] AdminResponse request, int id)
        {
            var result = await _adminService.UpdateAdmin(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminService.DeleteAdmin(id);
            return Ok(result);
        }

    }
}
