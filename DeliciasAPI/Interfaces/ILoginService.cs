using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface ILoginService
    {
        public Task<Response<User>> Authenticate(LoginResponse request);

    }
}
