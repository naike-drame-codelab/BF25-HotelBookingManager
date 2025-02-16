﻿using BookingManager.Application.Abstractions;
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
    }
}
