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

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
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
