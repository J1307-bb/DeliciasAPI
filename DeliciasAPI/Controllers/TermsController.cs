using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TermsController : ControllerBase
    {
        public readonly ITermsService _termsService;

        public TermsController(ITermsService TermsService)
        {
            _termsService = TermsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTerms()
        {
            var result = await _termsService.GetTerms();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneTerm(int id)
        {
            var result = await _termsService.GetTerm(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTerm([FromBody] TermsResponse request)
        {
            var result = await _termsService.CreateTerm(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTerms([FromBody] TermsResponse request, int id)
        {
            var result = await _termsService.UpdateTerms(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerms(int id)
        {
            var result = await _termsService.DeleteTerm(id);
            return Ok(result);
        }
    }
}
