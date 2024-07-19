using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMailService _mailService;

        public ContactService(ApplicationDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        // Lista de Contacts
        public async Task<Response<List<Contact>>> GetContacts()
        {
            try
            {
                List<Contact> response = await _context.Contacts.ToListAsync();

                return new Response<List<Contact>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Obtener un solo Contact
        public async Task<Response<Contact>> GetOneContact(int id)
        {
            try
            {
                Contact response = await _context.Contacts.FirstOrDefaultAsync(x => x.IdContact == id);
                return new Response<Contact>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear Contact
        public async Task<Response<Contact>> CreateContact(ContactResponse request)
        {
            try
            {
                Contact contact = new Contact()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Subject = request.Subject,
                    Message = request.Message,
                };

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                string htmlContent = @"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Gracias por Contactarnos</title>
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
                                <h1>¡Gracias por Contactarnos!</h1>
                                <p>Hola " + contact.Name + @",</p>
                                <p>Gracias por ponerte en contacto con Delicias. Hemos recibido tu mensaje y uno de los miembros de nuestro equipo se pondrá en contacto contigo muy pronto.</p>
                                <p>Si tienes alguna pregunta adicional o necesitas asistencia urgente, no dudes en contactarnos directamente a través de nuestro teléfono o correo electrónico.</p>
                                <p>Agradecemos tu interés en nuestros servicios y esperamos poder asistirte de la mejor manera posible.</p>
                                <p>Saludos cordiales,</p>
                                <p>El equipo de Delicias</p>
                            </div>
                            <div class='footer'>
                                <p>&copy; 2024 Delicias. Todos los derechos reservados.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Delicias-logo.svg");

                _mailService.SendMail(contact.Email, "Gracias por Contactarnos ☎️", htmlContent, logoPath);

                return new Response<Contact>(contact);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar Contact
        public async Task<Response<Contact>> UpdateContact(int id, ContactResponse response)
        {
            try
            {
                Contact con = await _context.Contacts.FirstOrDefaultAsync(x => x.IdContact == id);

                if (con != null)
                {
                    con.Name = response.Name;
                    con.Email = response.Email;
                    con.Subject = response.Subject;
                    con.Message = response.Message;                  
                    _context.SaveChanges();
                }

                Contact newContact = new Contact()
                {
                    Name= response.Name,
                    Email= response.Email,
                    Subject = response.Subject,
                    Message = response.Message,
                };

                _context.Contacts.Update(con);
                await _context.SaveChangesAsync();

                return new Response<Contact>(newContact);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar Contact
        public async Task<Response<Contact>> DeleteContacts(int id)
        {
            try
            {
                Contact contact = await _context.Contacts.FirstOrDefaultAsync(x => x.IdContact == id);
                if (contact != null)
                {
                    _context.Contacts.Remove(contact);
                    await _context.SaveChangesAsync();
                    return new Response<Contact>("Contact eliminado:" + contact.IdContact.ToString());
                }

                return new Response<Contact>("Contact no encontrado: " + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }

    }
}
