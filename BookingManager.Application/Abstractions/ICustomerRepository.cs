using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManager.DAL.Entities;

namespace BookingManager.Application.Abstractions
{
    public interface ICustomerRepository : ICrudRepository<Customer>
    {
        List<Customer> FindByKeyword(string? keyword);
        List<Customer> GetByYear(int year);
        Customer? GetByEmail(string email);
        //void Create(string lastName, string firstName, string email, string? phoneNumber);
    }
}
