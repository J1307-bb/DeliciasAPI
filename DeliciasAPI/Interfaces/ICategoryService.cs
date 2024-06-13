using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface ICategoryService
    {
        public Task<Response<List<Category>>> ObtenerCategories();
        public Task<Response<Category>> CrearCategory(CategoryResponse request);
        public Task<Response<Category>> ActualizarCategory(int id, CategoryResponse category);
        public Task<Response<Category>> EliminarCategory(int id);

    }
}
