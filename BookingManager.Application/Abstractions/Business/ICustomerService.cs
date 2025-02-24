using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManager.DAL.Entities;

namespace BookingManager.Application.Abstractions.Business
{
    public interface ICustomerService
    {
        public List<Customer> GetBySearch(string? search);
        public Customer CreateCustomer(Customer c);
        public void UpdateCustomer(int id, string lastName, string firstName, string? password, string? phoneNumber);
        public void DeleteCustomer(int id);
        IEnumerable<Customer> FindByKeyword(string? search);
        public Customer? GetCustomer(int id);
    }
}
