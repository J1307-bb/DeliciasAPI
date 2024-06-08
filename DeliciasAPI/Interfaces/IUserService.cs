using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface IUserService
    {
        public Task<Response<List<User>>> GetUsers();
        public Task<Response<User>> GetOneUser(int id);
        public Task<Response<User>> PostNewUser(UserDto user);
        public Task<Response<User>> UpdateUser(int id, UserDto user);
        public Task<Response<User>> DeleteUser(int id);
    }
}
