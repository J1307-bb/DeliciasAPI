using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealController : ControllerBase
    {
        public readonly IMealService _mealService;
        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMeals()
        {
            var result = await _mealService.ObtenerMeals();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ObtenerMeal(int id)
        {
            var result = await _mealService.ObtenerMeal(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MealResponse request)
        {
            var result = await _mealService.CrearMeal(request);
            return Ok(result);

        }

        [HttpPut("id")]
        public async Task<IActionResult> Actualizar([FromBody] MealResponse request, int id)
        {
            var result = await _mealService.ActualizarMeal(id, request);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _mealService.EliminarMeal(id);
            return Ok(result);
        }
    }
}
