using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de Orders
        public async Task<Response<List<Order>>> ObtenerOrders()
        {
            try
            {
                List<Order> response = await _context.Orders.Include(x => x.Meal).Include(x=> x.User).ToListAsync();
                return new Response<List<Order>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Obtener un solo Order
        public async Task<Response<Order>> ObtenerOrder(int id)
        {
            try
            {
                Order response = await _context.Orders.FirstOrDefaultAsync(x => x.IdOrder == id);
                return new Response<Order>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear Order
        public async Task<Response<Order>> CrearOrder(OrderResponse request)
        {
            try
            {
                Order order = new Order()
                {
                    NumMeals = request.NumMeals,
                    IdUser = request.IdUser,
                    IdMeal = request.IdMeal,
                    Date = request.Date,
                    Hour = request.Hour,
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return new Response<Order>(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar Order
        public async Task<Response<Order>> ActualizarOrder(int id, OrderResponse order)
        {
            try
            {
                Order ord = await _context.Orders.FirstOrDefaultAsync(x => x.IdOrder == id);

                if (ord != null)
                {
                    ord.NumMeals = order.NumMeals;
                    ord.IdUser = order.IdUser;
                    ord.Date = order.Date;
                    ord.Hour = order.Hour;
                    ord.IdMeal = order.IdMeal;
                    _context.SaveChanges();
                }

                Order newOrder = new Order()
                {
                    NumMeals = order.NumMeals,
                    IdUser = order.IdUser,
                    IdMeal = order.IdMeal,
                    Date = order.Date,
                    Hour = order.Hour,
                };

                _context.Orders.Update(ord);
                await _context.SaveChangesAsync();

                return new Response<Order>(newOrder);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar Order
        public async Task<Response<Order>> EliminarOrder(int id)
        {
            try
            {
                Order ord = await _context.Orders.FirstOrDefaultAsync(x => x.IdOrder == id);
                if (ord != null)
                {
                    _context.Orders.Remove(ord);
                    await _context.SaveChangesAsync();
                    return new Response<Order>("Usuario eliminado:" + ord.IdOrder.ToString());
                }

                return new Response<Order>("Order no encontrado: " + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }


    }
}
