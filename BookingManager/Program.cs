using BookingManager.DAL;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

// using : permet de fermer ma connexion, car context = IDisposable
using HotelContext ctx = new HotelContext();

// récupérer les données
// SELECT * FROM Room
List<Room> result = ctx.Rooms.ToList();

// SELECT * FROM Room WHERE Surface > 100
result = ctx.Rooms.Where(r => r.Surface > 100).ToList();

// SELECT * FROM Room WHERE Id = 42
// nullable car pas sûr de la trouver
Room? room = ctx.Rooms.Find(42);

// SELECT * FROM example WHERE ID1 = 45 AND ID2 = 73
// si clé composite (plusieurs ids), les écrire
// var ex = ctx.Rooms.Find(45, 73);

// trouver le premier
// SELECT * FROM Customer WHERE email LIKE 'lykhun@gmail.com'
// si on utilise .Single() ou .First(), le système plante s'il ne trouve pas --> ...OrDefault() variant
Customer? user = ctx.Customers.SingleOrDefault(c => c.Email == "lykhun@gmail.com");
// permet de chercher des patterns avec ce Like() et ignore la casse et les accents, mais va plaknter s'il trouve plusieurs résultats
Customer? user2 = ctx.Customers.SingleOrDefault(c => EF.Functions.Like(c.Email, "%@gmail.com"));

// SELECT TOP 1 * FROM Customer WHERE lastName LIKE "a%" --> 1er dont nom commence par a
Customer? user3 = ctx.Customers.FirstOrDefault(c => c.LastName.StartsWith("a"));
Customer? user4 = ctx.Customers.FirstOrDefault(c => EF.Functions.Like(c.LastName, "a%"));

// Passer 10 lignes et récupérer les 10 suivantes
// SELECT * FROM Customer
// ORDER BY LastName
// OFFSET 10 ROWS FETCH 10 ROWS ONLY 
List<Customer> result2 = ctx.Customers
    .OrderBy(c => c.LastName)
    .Skip(10).Take(10).ToList();

// récupérer le nombre de customers
// SELECT COUNT(*) FROM Customer
int count = ctx.Customers.Count();

// récupérer la moyenne des prix des chambres
// SELECT AVG(price) FROM Room
// decimal average = ctx.Rooms.Average(r => r.Price);

// jointures
// le GROUP BY est fait auto par défaut
// récupérer la chambre liéée à une réservation d'un client
// SELECT c.*, b.*, r.*
// FROM Customer c
// LEFT Booking b on b.CustomerId = c.CustomerId
// LEFT JOIN r ON b.RoomId = r.RoomId
// depuis mes customers, je joins les bookings et enfin j'inclus la room : .Include() puis .ThenInclude() pour spécifier que je place le point de départ sur booking
// si je pars de booking, j'utilise 2 .Include() comme jointures
// si je pars de room : .Include() sur booking puis .ThenInclude() sur customer
List<Customer> result3 = ctx.Customers
    .Include(c => c.Bookings)
    .ThenInclude(b => b.Room)
    .ToList();

// DML Insert, Update, Delete
// INSERT INTO Customer
// (LastName, Firstname, Email, Password, PhoneNumber, UserName)
// VALUES
// ('LY', 'Khun', 'lykhun@gmail.com', [], null, 'K')
//Customer customer = new Customer
//{
//    LastName = "Ly",
//    FirstName = "Khun",
//    Email = "lykhun@gmail.com",
//    Password = new byte[0],
//    PhoneNumber = null,
//    Username = "K"
//};
//ctx.Customers.Add(customer);
//ctx.SaveChanges();

//Booking b = new Booking
//{
//    BookingDate = DateTime.Now,
//    StartDate = DateTime.Now,
//    EndDate = DateTime.Now.AddDays(42),
//    Status = BookingManager.DAL.Enums.BookingStatus.InProgress,
//    // dans le cas où l'on souhaite créer le customer en même temps que la réservation
//    // Customer = new Customer {/*LastName = "Ly",...*/}
//    // dans le cas où l'on souhaite utiliser un customer existant
//    CustomerId = 1,
//    RoomId = 42
//};
//ctx.Bookings.Add(b);
//ctx.SaveChanges();

//List<Option> options = 
//    [
//        new Option { Name = "Vue sur Mer"},
//        new Option { Name = "Suite"},
//        new Option { Name = "Air conditionné"},
//        new Option { Name = "Mini Bar"},
//        new Option { Name = "Jaccuzzi"},
//        new Option { Name = "Casino"},
//    ];
////insertions multiples
//ctx.Options.AddRange(options);

//Room r = new Room
//{
//    Floor = 42,
//    ImageUrl = "",
//    MaxCapacity = 2,
//    Number = "42A",
//    Price = 42,
//    Surface = 42,
//    // ajoute dans la table intermédiaire entre Room et Option
//    Options = [options[0], options[2], options[4]] 
//};
//ctx.Rooms.Add(r);
//// sauvegarder les changements faits
//ctx.SaveChanges();

//// modification
//// UPDATE Customer SET LastName = "Lee" WHERE CustomerId = 1
//Customer? c = ctx.Customers.Find(1);
//c.LastName = "Lee";
//c.FirstName = "Simon";
//ctx.SaveChanges();

//// suppression
//// DELETE FROM Customer Where Id = 1
//Customer? c = ctx.Customers.Find(1);
//if (c != null) ctx.Remove(c);
//ctx.SaveChanges();

//// moyenne des prix par étage
//// SELECT AVG(price) FROM Room GROUP BY floor
//// 1) grouper les rooms par étage, 2) récupérer liste de prix pour le groupe
// List<decimal> avgPerFoor = ctx.Rooms.GroupBy(r => r.Floor).Select(g => g.Average(r => r.Price)).ToList()

/// SELECT floor AS Floor, AVG(price) AS Average FROM Room GROUP BY floor
var avgPerFoor = ctx.Rooms.GroupBy(r => r.Floor)
    //.Select(g => g.Average(r => r.Price))
    .ToList()
    .Select(g => new
    {
        Floor = g.Key,
        Average = g.Average(r => r.Price)
    })
    .ToList();


foreach (var avg in avgPerFoor)
{
    Console.WriteLine(avg.Floor);
    Console.WriteLine(avg.Average);
}