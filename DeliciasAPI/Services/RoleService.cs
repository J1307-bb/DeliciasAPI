using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class RoleService : IRoleService
    {

        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de Roles
        public async Task<Response<List<Role>>> GetRoles()
        {
            try
            {
                List<Role> response = await _context.Roles.ToListAsync();
                return new Response<List<Role>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Obtener un solo Role
        public async Task<Response<Role>> GetRole(int id)
        {
            try
            {
                Role response = await _context.Roles.FirstOrDefaultAsync(x => x.IdRole == id);
                return new Response<Role>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear un Role
        public async Task<Response<Role>> CreateRole(RoleResponse request)
        {
            try
            {
                Role role = new Role()
                {
                    NameRole = request.NameRole,
                };

                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                return new Response<Role>(role);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar Role
        public async Task<Response<Role>> UpdateRole(int id, RoleResponse request)
        {
            try
            {
                Role role = await _context.Roles.FirstOrDefaultAsync(x => x.IdRole == id);

                if (role != null)
                {
                    role.NameRole = request.NameRole;
                    _context.SaveChanges();
                }

                Role newRole = new Role()
                {
                    NameRole = role.NameRole
                };

                _context.Roles.Update(role);
                await _context.SaveChangesAsync();

                return new Response<Role>(newRole);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar Role
        public async Task<Response<Role>> DeleteRole(int id)
        {
            try
            {
                Role role = await _context.Roles.FirstOrDefaultAsync(x => x.IdRole == id);
                if (role != null)
                {
                    _context.Roles.Remove(role);
                    await _context.SaveChangesAsync();
                    return new Response<Role>("Role eliminado:" + role.NameRole.ToString());
                }

                return new Response<Role>("Role no encontrado" + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }

    }
}
