using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DeliciasAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMailService _mailService;

        public UserService(ApplicationDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        // Lista de Users
        public async Task<Response<List<User>>> ObtenerUsers()
        {
            try
            {
                List<User> response = await _context.Users.Include(x => x.Role).ToListAsync();
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
                    IdRole = request.IdRole,
                };

                _context.Users.Add(usuario);
                await _context.SaveChangesAsync();

                string body = @"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Bienvenido(a)</title>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }
                            .container {
                                width: 100%;
                                max-width: 600px;
                                margin: 0 auto;
                                background-color: #ffffff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            }
                            .header {
                                text-align: center;
                                padding: 10px 0;
                            }
                            .header img {
                                max-width: 100px;
                            }
                            .content {
                                margin: 20px 0;
                                line-height: 1.6;
                            }
                            .content h1 {
                                color: #333333;
                            }
                            .content p {
                                color: #555555;
                            }
                            .footer {
                                text-align: center;
                                margin: 20px 0;
                                color: #777777;
                                font-size: 12px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <img src='cid:logo' alt='Delicias Logo'>
                            </div>
                            <div class='content'>
                                <h1>¡Bienvenido a Delicias!</h1>
                                <p>Hola " + usuario.Name + ' ' + usuario.LastName + @",</p>
                                <p>Gracias por registrarte en Delicias. Estamos encantados de tenerte con nosotros.</p>
                                <p>En Delicias, nos esforzamos por ofrecerte los mejores platillos y una experiencia inigualable.</p>
                                <p>Si tienes alguna pregunta o necesitas ayuda, no dudes en contactarnos.</p>
                                <p>¡Esperamos que disfrutes de nuestra oferta culinaria!</p>
                                <p>Saludos cordiales,</p>
                                <p>El equipo de Delicias</p>
                            </div>
                            <div class='footer'>
                                <p>&copy; 2024 Delicias. Todos los derechos reservados.</p>
                            </div>
                        </div>
                    </body>
                    </html>"
                ;

                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "DeliciasAPI\\wwwroot\\images\\DeliciasLogo.svg");
                _mailService.SendMail(usuario.Email, "Bienvenido a Delicias.com", body, logoPath);

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
                    us.IdRole = usuario.IdRole;
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
                    IdRole = usuario.IdRole,
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
