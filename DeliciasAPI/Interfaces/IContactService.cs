using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface IContactService
    {
        public Task<Response<List<Contact>>> GetContacts();
        public Task<Response<Contact>> GetOneContact(int id);
        public Task<Response<Contact>> CreateContact(ContactResponse request);
        public Task<Response<Contact>> UpdateContact(int id, ContactResponse response);
        public Task<Response<Contact>> DeleteContacts(int id);

    }
}
