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

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    // combien de temps ma session subsiste sans activité = après x secondes/minutes/heures d'inactivité, je dois me reco
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.HttpOnly = true;
    // si mon cookie n'a pas été set, mon app ne tourne plus
    options.Cookie.IsEssential = true;
});

// Config HTTP request
var app = builder.Build();

// Configure the HTTP request pipeline = middlewares.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// on peut ajouter des routes ici
// comme les middlewares s'exécutent les uns après les autres, et comme le programme va trouver une réponse, on ne verra pas Hello World mais Machin
// app.MapGet("/test/hello", () => "Machin");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
