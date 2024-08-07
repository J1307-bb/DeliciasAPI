﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<QuoteItem> QuoteItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Insertar en la tabla Quotes
            modelBuilder.Entity<QuoteItem>()
                .HasOne(qm => qm.Quote)
                .WithMany(q => q.QuoteItems)
                .HasForeignKey(qm => qm.IdQuote);

            modelBuilder.Entity<QuoteItem>()
                .HasOne(qm => qm.Meal)
                .WithMany()
                .HasForeignKey(qm => qm.IdMeal);

            modelBuilder.Entity<OrderItem>()
                .HasOne(qm => qm.Order)
                .WithMany(q => q.OrderItems)
                .HasForeignKey(qm => qm.IdOrder);

            modelBuilder.Entity<OrderItem>()
                .HasOne(qm => qm.Meal)
                .WithMany()
                .HasForeignKey(qm => qm.IdMeal);


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
                    UrlPP = "imagen.jpg",
                    IdRole = 1
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    IdRole = 1,
                    NameRole = "SuperAdmin"
                },
                new Role()
                {
                    IdRole = 2,
                    NameRole = "Admin"
                },
                new Role()
                {
                    IdRole = 3,
                    NameRole = "User"
                }
            );

        }
    }
}
