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

        public void DeleteCustomer(int id);
    }
}
