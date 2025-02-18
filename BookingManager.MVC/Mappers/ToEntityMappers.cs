using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManager.DAL.Entities;
using BookingManager.MVC.Models;
using BookingManager.MVC.Utilitaries;

namespace BookingManager.MVC.Mappers
{
    public static class ToEntityMappers
    {
        public static Customer ToCustomerIndex(this CustomerRegisterFormViewModel model)
        {

            byte[] password = [255];           
            GeneratePassword generatePassword = new GeneratePassword();
            generatePassword.RandomPassword(10);
            Console.WriteLine(generatePassword);

            string username = model.LastName[..2] + model.FirstName[..2];

            return new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Username = username,
                Password = password
            };
        }
    }
}
