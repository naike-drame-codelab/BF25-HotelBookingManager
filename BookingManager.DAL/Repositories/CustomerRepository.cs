using BookingManager.Application.Abstractions.Repositories;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.DAL.Repositories
{
    public class CustomerRepository(HotelContext ctx): CrudRepositoryBase<Customer>(ctx), ICustomerRepository
    {
        // écrasement de la méthode de base de CrudRepository pour inclure une jointure avec la table Bookings
        public override List<Customer> GetAll()
        {
            return ctx.Customers
                .Where(c => c.Deleted == false)
                .Include(c => c.Bookings)
                .ToList();
        }

        // string? car potentiellement null / on ne met rien dans la barre de recherche
        public List<Customer> FindByKeyword(string? keyword)
        {
            return ctx.Customers
                .Include(c => c.Bookings)
                .Where(c =>
                keyword == null
                || c.LastName.Contains(keyword)
                || c.FirstName.Contains(keyword)
                || c.Email.Contains(keyword)
                && c.Deleted == false
                )
                .ToList();
        }

        public Customer? GetByEmail(string email)
        {
            return ctx.Customers.Where(c => c.Email == email).FirstOrDefault();
        }

        public List<Customer> GetByYear(int year)
        {
            return ctx.Customers
                .Where(c => c.Bookings
                    .Any(b => b.BookingDate.Year == year))
                .ToList();
        }

        public override void Remove(Customer c)
        {
            Customer? cu = ctx.Customers.Find(c.LoginId);
            cu.Deleted = true;
            Console.WriteLine(cu.Deleted);
            cu.PhoneNumber = null;
            Console.WriteLine(cu.PhoneNumber);
            ctx.SaveChanges();
        }

        public int CountByUsername(string prefix)
        {
            return ctx.Customers.Count(c => c.Username.StartsWith(prefix));
        }
    }
}
