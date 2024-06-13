using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de Categorias
        public async Task<Response<List<Category>>> ObtenerCategories()
        {
            try
            {
                List<Category> response = await _context.Categories.ToListAsync();
                return new Response<List<Category>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear un Meal
        public async Task<Response<Category>> CrearCategory(CategoryResponse request)
        {
            try
            {
                Category category = new Category()
                {
                    NameCategory = request.NameCategory
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return new Response<Category>(category);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }

        }

        // Actualizar Category
        public async Task<Response<Category>> ActualizarCategory(int id, CategoryResponse category)
        {
            try
            {
                Category ca = await _context.Categories.FirstOrDefaultAsync(x => x.IdCategory == id);

                if (ca != null)
                {
                    ca.NameCategory = category.NameCategory;
                    _context.SaveChanges();
                }

                Category newCategory = new Category()
                {
                    NameCategory = category.NameCategory,
                };

                _context.Categories.Update(ca);
                await _context.SaveChangesAsync();

                return new Response<Category>(newCategory);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar Meal
        public async Task<Response<Category>> EliminarCategory(int id)
        {
            try
            {
                Category ca = await _context.Categories.FirstOrDefaultAsync(x => x.IdCategory == id);
                if (ca != null)
                {
                    _context.Categories.Remove(ca);
                    await _context.SaveChangesAsync();
                    return new Response<Category>("Category eliminado:" + ca.NameCategory.ToString());
                }

                return new Response<Category>("Category no encontrado" + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }

    }
}
