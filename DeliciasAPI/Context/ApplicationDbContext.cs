using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Domain.Entities;

namespace DeliciasAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        //Modelos
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Insertar en la tabla usuario
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    IdUser = 1,
                    Name = "Jair",
                    LastName = "Badillo",
                    Password = "123456",
                    Email = "jair@gmail.com",
                    PhoneNumber = "1234567890",
                    UrlPP = "imagen.jpg"
                }
            );

        }
    }
}
