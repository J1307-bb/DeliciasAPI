using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        public readonly IQuotesService _quotesService;
        public QuoteController(IQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _quotesService.GetQuotes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _quotesService.GetQuote(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuoteResponse request)
        {
            var result = await _quotesService.CreateQuote(request);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] QuoteResponse request, int id)
        {
            var result = await _quotesService.UpdateQuote(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _quotesService.DeleteQuote(id);
            return Ok(result);
        }

    }
}
