using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface IMealService
    {
        public Task<Response<List<Meal>>> ObtenerMeals();
        public Task<Response<Meal>> ObtenerMeal(int id);
        public Task<Response<Meal>> CrearMeal(MealResponse request);
        public Task<Response<Meal>> ActualizarMeal(int id, MealResponse meal);
        public Task<Response<Meal>> EliminarMeal(int id);

    }
}
