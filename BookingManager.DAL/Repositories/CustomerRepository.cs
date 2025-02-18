using System.Security.Cryptography;
using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.DAL.Repositories
{
    public class CustomerRepository : CrudRepositoryBase<Customer>, ICustomerRepository
    {
        // écrasement de la méthode de base de CrudRepository pour inclure une jointure avec la table Bookings
        public override List<Customer> GetAll()
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Customers.Include(c => c.Bookings).ToList();
        }

        // string? car potentiellement null / on ne met rien dans la barre de recherche
        public List<Customer> FindByKeyword(string? keyword)
        {
            using HotelContext ctx = new HotelContext();

            return ctx.Customers
                .Include(c => c.Bookings)
                .Where(c =>
                keyword == null
                || c.LastName.Contains(keyword)
                || c.FirstName.Contains(keyword)
                || c.Email.Contains(keyword)
                )
                .ToList();
        }

        public Customer? GetByEmail(string email)
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Customers.Where(c => c.Email == email).FirstOrDefault();
        }

        public List<Customer> GetByYear(int year)
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Customers
                .Where(c => c.Bookings
                    .Any(b => b.BookingDate.Year == year))
                .ToList();
        }

        public override Customer Add(Customer c)
        {
            using HotelContext ctx = new HotelContext();
            
            Customer customer = new Customer
            {
                LastName = c.LastName,
                FirstName = c.FirstName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Username = c.Username,
                Password = c.Password
            };
            ctx.Customers.Add(c);
            ctx.SaveChanges();

            return c;
        }
    }
}
