using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface IAdminService
    {
        public Task<Response<List<Admin>>> GetAdmins();
        public Task<Response<Admin>> GetAdmin(int id);
        public Task<Response<Admin>> CreateAdmin(AdminResponse request);
        public Task<Response<Admin>> UpdateAdmin(int id, AdminResponse request);
        public Task<Response<Admin>> DeleteAdmin(int id);

    }
}
