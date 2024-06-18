using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface IRoleService
    {
        public Task<Response<List<Role>>> GetRoles();
        public Task<Response<Role>> GetRole(int id);
        public Task<Response<Role>> CreateRole(RoleResponse request);
        public Task<Response<Role>> UpdateRole(int id, RoleResponse request);
        public Task<Response<Role>> DeleteRole(int id);

    }
}
