using DeliciasAPI.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerOrders()
        {
            var result = await _orderService.ObtenerOrders();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerOrder(int id)
        {
            var result = await _orderService.ObtenerOrder(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] OrderResponse request)
        {
            var result = await _orderService.CrearOrder(request);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] OrderResponse request, int id)
        {
            var result = await _orderService.ActualizarOrder(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _orderService.EliminarOrder(id);
            return Ok(result);
        }

    }
}
