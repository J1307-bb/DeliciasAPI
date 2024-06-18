using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de Admins
        public async Task<Response<List<Admin>>> GetAdmins()
        {
            try
            {
                List<Admin> response = await _context.Admins.Include(x => x.Role).ToListAsync();
                return new Response<List<Admin>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Obtener un solo Admin
        public async Task<Response<Admin>> GetAdmin(int id)
        {
            try
            {
                Admin response = await _context.Admins.FirstOrDefaultAsync(x => x.IdAdmin == id);
                return new Response<Admin>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear un Admin
        public async Task<Response<Admin>> CreateAdmin(AdminResponse request)
        {
            try
            {
                Admin admin = new Admin()
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password,
                    IdRole =  request.IdRole
                };

                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();

                return new Response<Admin>(admin);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar Admin
        public async Task<Response<Admin>> UpdateAdmin(int id, AdminResponse request)
        {
            try
            {
                Admin admin = await _context.Admins.FirstOrDefaultAsync(x => x.IdAdmin == id);

                if (admin != null)
                {
                    admin.Name = request.Name;
                    admin.LastName = request.LastName;
                    admin.Email = request.Email;
                    admin.Password = request.Password;
                    admin.IdRole = request.IdRole;
                    _context.SaveChanges();
                }

                Admin newAdmin = new Admin()
                {
                    Name = admin.Name,
                    LastName = admin.LastName,
                    Email = admin.Email,
                    Password = admin.Password,
                    IdRole = admin.IdRole
                };

                _context.Admins.Update(admin);
                await _context.SaveChangesAsync();

                return new Response<Admin>(newAdmin);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar Admin
        public async Task<Response<Admin>> DeleteAdmin(int id)
        {
            try
            {
                Admin admin = await _context.Admins.FirstOrDefaultAsync(x => x.IdAdmin == id);
                if (admin != null)
                {
                    _context.Admins.Remove(admin);
                    await _context.SaveChangesAsync();
                    return new Response<Admin>("Admin eliminado:" + admin.Name.ToString());
                }

                return new Response<Admin>("Admin no encontrado" + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }
    }
}
