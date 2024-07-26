using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

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
                List<Order> orders = await _context.Orders
                .Include(q => q.User)
                .Include(q => q.OrderItems)
                    .ThenInclude(qi => qi.Meal)
                .ToListAsync();

                return new Response<List<Order>>(orders);
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
                    Place = request.Place,
                    NumMeals = request.NumMeals,
                    Date = request.Date,
                    Hour = request.Hour,
                    IdUser = request.IdUser,
                    Status = request.Status,
                    TotalPrice = request.TotalPrice,
                    OrderItems = request.OrderItems.Select(qi => new OrderItem
                    {
                        IdMeal = qi.IdMeal,
                        Quantity = qi.Quantity
                    }).ToList()
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

                Order ord = await _context.Orders
                     .Include(q => q.OrderItems)
                     .FirstOrDefaultAsync(x => x.IdOrder == id);

                if (ord == null)
                {
                    return new Response<Order>("Order not found.");
                }

                ord.Place = order.Place;
                ord.NumMeals = order.NumMeals;
                ord.Status = order.Status;
                ord.Date = order.Date;
                ord.Hour = order.Hour;
                ord.IdUser = order.IdUser;
                ord.TotalPrice = order.TotalPrice;

                // Actualizar los QuoteItems
                ord.OrderItems.Clear();
                foreach (var item in order.OrderItems)
                {
                    ord.OrderItems.Add(new OrderItem
                    {
                        IdMeal = item.IdMeal,
                        Quantity = item.Quantity
                    });
                }

                _context.Orders.Update(ord);
                await _context.SaveChangesAsync();

                return new Response<Order>(ord);

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
                Order ord = await _context.Orders
                    .Include(q => q.OrderItems)
                    .FirstOrDefaultAsync(x => x.IdOrder == id);
                if (ord != null)
                {
                    _context.Orders.Remove(ord);
                    await _context.SaveChangesAsync();
                    return new Response<Order>("Orden eliminado:" + ord.IdOrder.ToString());
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
