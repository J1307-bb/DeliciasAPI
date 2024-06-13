using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext _context;

        public MealService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de Meals
        public async Task<Response<List<Meal>>> ObtenerMeals()
        {
            try
            {
                List<Meal> response = await _context.Meals.Include(x => x.Category).ToListAsync();
                return new Response<List<Meal>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Obtener un solo Meal
        public async Task<Response<Meal>> ObtenerMeal(int id)
        {
            try
            {
                Meal response = await _context.Meals.FirstOrDefaultAsync(x => x.IdMeal == id);
                return new Response<Meal>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear un Meal
        public async Task<Response<Meal>> CrearMeal(MealResponse request)
        {
            try
            {
                Meal meal = new Meal()
                {
                    Title = request.Title,
                    Description = request.Description,
                    Ingredients = request.Ingredients,
                    Status = request.Status,
                    Price = request.Price,
                    UrlImage = request.UrlImage,
                    IdCategory = request.IdCategory,
                };

                _context.Meals.Add(meal);
                await _context.SaveChangesAsync();

                return new Response<Meal>(meal);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar Meal
        public async Task<Response<Meal>> ActualizarMeal(int id, MealResponse meal)
        {
            try
            {
                Meal me = await _context.Meals.FirstOrDefaultAsync(x => x.IdMeal == id);

                if (me != null)
                {
                    me.Title = meal.Title;
                    me.Description = meal.Description;
                    me.Ingredients = meal.Ingredients;
                    me.Status = meal.Status;
                    me.Price = meal.Price;
                    me.UrlImage = meal.UrlImage;
                    me.IdCategory = meal.IdCategory;
                    _context.SaveChanges();
                }

                Meal newMeal = new Meal()
                {
                    Title = meal.Title,
                    Description = meal.Description,
                    Ingredients = meal.Ingredients,
                    Status = meal.Status,
                    Price = meal.Price,
                    UrlImage = meal.UrlImage,
                    IdCategory = meal.IdCategory,
                };

                _context.Meals.Update(me);
                await _context.SaveChangesAsync();

                return new Response<Meal>(newMeal);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar Meal
        public async Task<Response<Meal>> EliminarMeal(int id)
        {
            try
            {
                Meal me = await _context.Meals.FirstOrDefaultAsync(x => x.IdMeal == id);
                if (me != null)
                {
                    _context.Meals.Remove(me);
                    await _context.SaveChangesAsync();
                    return new Response<Meal>("Meal eliminado:" + me.Title.ToString());
                }

                return new Response<Meal>("Meal no encontrado" + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }

    }
}
