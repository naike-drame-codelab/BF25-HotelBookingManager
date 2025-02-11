using BookingManager.Application.Abstractions;
using BookingManager.DAL;
using BookingManager.DAL.Entities;
using BookingManager.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

#region Ancienne version
//using HotelContext ctx = new HotelContext();

//// ajouter une nouvelle donnée
////await ctx.Options.AddAsync(new Option { Name = "Machine à laver" });
////await ctx.SaveChangesAsync();

//// methode main() devient asynchrone si j'utilise await
//// List<Booking> result = await ctx.Bookings.ToListAsync();

//// retour sur le thread principal avec .Result
////List<Booking> result = ctx.Bookings.ToListAsync().Result;

//foreach (Option o in ctx.Options.ToList())
//{
//    Console.WriteLine(o.Name);
//} 
#endregion

#region Application du Repository Design Pattern
ICustomerRepository customerRepository = new CustomerRepository();
IOptionRepository optionRepository = new OptionRepository();

List<Customer> customers = customerRepository.GetAll();
List<Option> options = optionRepository.GetAll();
Option? option = optionRepository.GetById(4);
optionRepository.Remove(option);


foreach (Customer customer in customers)
{
	Console.WriteLine(customer.LastName);
}
Console.WriteLine(option?.Name);
Console.WriteLine(string.Join(", ", options.Select(o => o.Name)));


#endregion