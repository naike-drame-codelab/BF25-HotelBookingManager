using BookingManager.DAL;
using BookingManager.MVC.Configurations;
using Microsoft.EntityFrameworkCore;

// Config creation builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSmtp(builder.Configuration);

builder.Services.AddDbContext<HotelContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Main"));
});

builder.Services.AddRepositories();

builder.Services.AddServices();

// Config HTTP request
var app = builder.Build();

// Configure the HTTP request pipeline = middlewares.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// on peut ajouter des routes ici
// comme les middlewares s'exécutent les uns après les autres, et comme le programme va trouver une réponse, on ne verra pas Hello World mais Machin
// app.MapGet("/test/hello", () => "Machin");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
