using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<User>>> GetUsers()
        {
            try
            {
                List<User> users = await _context.Users.ToListAsync();

                var result = new Response<List<User>>(users, "succesful request");

                return result;

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<User>> GetOneUser(int id)
        {
            try
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.Equals(id));

                var result = new Response<User>(user);

                return result;

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<User>> PostNewUser(UserDto userDto)
        {
            try
            {
                User newUser = new User()
                {
                    Name = userDto.Name,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    PhoneNumber = userDto.PhoneNumber,
                    UrlPP = userDto.UrlPP,
                };

                _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                var result = new Response<User>(newUser, "succesful request");

                return result;

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<User>> UpdateUser(int id, UserDto user) 
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("id doesnt exist");
                }

                User userNew = await _context.Users.SingleOrDefaultAsync(x => x.Equals(id));

                userNew.Name = user.Name;
                userNew.LastName = user.LastName;
                userNew.Email = user.Email;
                userNew.Password = user.Password;
                userNew.PhoneNumber = user.PhoneNumber;
                userNew.UrlPP = user.UrlPP;

                await _context.SaveChangesAsync();

                var result = new Response<User>(userNew, "succesful request");

                return result;

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<User>> DeleteUser(int id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("id doesnt exist");
                }

                User userToDelete = await _context.Users.SingleOrDefaultAsync(x => x.Equals(id));

                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();

                var result = new Response<User>(userToDelete, "User deleted succesfully");
                
                return result;

            } catch (Exception ex) { 
                throw ex;
            }
        }
    }
}
