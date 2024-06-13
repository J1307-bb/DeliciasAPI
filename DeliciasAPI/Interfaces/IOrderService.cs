using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface IOrderService
    {
        public Task<Response<List<Order>>> ObtenerOrders();
        public Task<Response<Order>> ObtenerOrder(int id);
        public Task<Response<Order>> CrearOrder(OrderResponse request);
        public Task<Response<Order>> ActualizarOrder(int id, OrderResponse order);
        public Task<Response<Order>> EliminarOrder(int id);

    }
}
