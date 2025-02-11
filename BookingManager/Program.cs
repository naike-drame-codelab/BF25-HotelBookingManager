using BookingManager.DAL;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

// using : permet de fermer ma connexion, car context = IDisposable
using HotelContext ctx = new HotelContext();

// chaque élément est traqué et après chaque .SaveChanges(), j'update la db
Customer customer = ctx.Customers.Find(1) ?? throw new Exception();
customer.LastName = "Ly";
ctx.SaveChanges();

Customer customer2 = ctx.Customers.ToList()[3];
customer2.LastName = "Person";
ctx.SaveChanges();

// parfois je veux consulter une liste en lecture seule sans la traquer
// le tracking prends du temps et de la mémoire
// si je veux simplement afficher les éléments chargés et non les modifier --> .AsNoTracking()
// améliore les performances, plus rapide
Customer customer3 = ctx.Customers.AsNoTracking().ToList()[3];
customer3.LastName = "Morre";
ctx.SaveChanges();