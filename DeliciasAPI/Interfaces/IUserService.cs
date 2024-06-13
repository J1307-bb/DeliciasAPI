using Domain.Entities;
using Domain.DTO;

namespace DeliciasAPI.Interfaces
{
    public interface IUserService
    {
        public Task<Response<List<User>>> ObtenerUsers();
        public Task<Response<User>> ObtenerUser(int id);
        public Task<Response<User>> CrearUser(UserResponse request);
        public Task<Response<User>> ActualizarUser(int id, UserResponse usuario);
        public Task<Response<User>> EliminarUser(int id);

    }
}
