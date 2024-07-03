using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategories()
        {
            var result = await _categoryService.ObtenerCategories();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CategoryResponse request)
        {
            var result = await _categoryService.CrearCategory(request);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] CategoryResponse request, int id)
        {
            var result = await _categoryService.ActualizarCategory(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _categoryService.EliminarCategory(id);
            return Ok(result);
        }
    }
}
