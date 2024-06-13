using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        public readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetContacts();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _contactService.GetOneContact(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactResponse request)
        {
            var result = await _contactService.CreateContact(request);
            return Ok(result);

        }

        [HttpPut("id")]
        public async Task<IActionResult> Update([FromBody] ContactResponse request, int id)
        {
            var result = await _contactService.UpdateContact(id, request);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactService.DeleteContacts(id);
            return Ok(result);
        }


    }
}
