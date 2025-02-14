var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
// comme les middlewares s'ex�cutent les uns apr�s les autres, et comme le programme va trouver une r�ponse, on ne verra pas Hello World mais Machin
// app.MapGet("/test/hello", () => "Machin");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
