using BookingManager.DAL.Entities;
using BookingManager.MVC.Models;

namespace BookingManager.MVC.Mappers
{
    public static class ToEntityMappers
    {
        public static Customer ToCustomerCreate(this CustomerCreateFormViewModel model)
        {
            return new Customer
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
        }
    }
}