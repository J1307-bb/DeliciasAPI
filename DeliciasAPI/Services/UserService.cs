using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de Users
        public async Task<Response<List<User>>> ObtenerUsers()
        {
            try
            {
                List<User> response = await _context.Users.ToListAsync();
                return new Response<List<User>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Obtener un solo User
        public async Task<Response<User>> ObtenerUser(int id)
        {
            try
            {
                User response = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                return new Response<User>(response);
            }
            catch (Exception ex) 
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear un User
        public async Task<Response<User>> CrearUser(UserResponse request)
        {
            try
            {
                User usuario = new User()
                {   
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    UrlPP = request.UrlPP,
                };

                _context.Users.Add(usuario);
                await _context.SaveChangesAsync();

                return new Response<User>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar User
        public async Task<Response<User>> ActualizarUser(int id, UserResponse usuario)
        {
            try
            {
                User us = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);

                if (us != null)
                {
                    us.Name = usuario.Name;
                    us.LastName = usuario.LastName;
                    us.Email = usuario.Email;
                    us.Password = usuario.Password;
                    us.PhoneNumber = usuario.PhoneNumber;
                    us.UrlPP = usuario.UrlPP;
                    _context.SaveChanges();
                }

                User newUsuario = new User()
                {
                    Name = usuario.Name,
                    LastName = usuario.LastName,
                    Email = usuario.Email,
                    Password = usuario.Password,
                    PhoneNumber = usuario.PhoneNumber,
                    UrlPP = usuario.UrlPP,
                };

                _context.Users.Update(us);
                await _context.SaveChangesAsync();

                return new Response<User>(newUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar User
        public async Task<Response<User>> EliminarUser(int id)
        {
            try
            {
                User us = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                if (us != null)
                {
                    _context.Users.Remove(us);
                    await _context.SaveChangesAsync();
                    return new Response<User>("Usuario eliminado:" + us.Name.ToString());
                }

                return new Response<User>("Usuario no encontrado" + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }

    }
}
