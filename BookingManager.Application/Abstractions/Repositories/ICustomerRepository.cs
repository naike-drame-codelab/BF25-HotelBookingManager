using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManager.DAL.Entities;

namespace BookingManager.Application.Abstractions.Repositories
{
    public interface ICustomerRepository : ICrudRepository<Customer>
    {
        List<Customer> FindByKeyword(string? keyword);
        List<Customer> GetByYear(int year);
        Customer? GetByEmail(string email);
        int CountByUsername(string prefix);
    }
}
