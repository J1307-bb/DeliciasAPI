using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<User>> Authenticate(LoginResponse request)
        {
            try
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

                if (user == null || user.Password != request.Password)
                {
                    return null;
                }

                return new Response<User>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }
    }
}
