using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BookingManager.DAL.Configurations;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.DAL
{
    public class HotelContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public HotelContext()
        {

        }

        public HotelContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // optionsBuilder.UseSqlServer("server=DESKTOP-E563U3H;database=hotel.db;integrated security=true;trustServerCertificate=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
           
            #if DEBUG
            modelBuilder.Entity<Login>().HasData([
                new Login {
                    LoginId  = -1,
                    Username = "admin",
                    Password = SHA512.HashData(Encoding.UTF8.GetBytes("1234"))
                }
            ]);
            #endif
        }
    }
}
