using LojaVeiculos.Data;
using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using LojaVeiculos.RabbitMq;
using LojaVeiculos.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IRabbitMq, RabbitMq>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();



string server = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (server == "Development")
{
    builder.Services.AddDbContext<Context>(options =>
                    options.UseLazyLoadingProxies().UseSqlServer($"Data Source=DESKTOP-V63D3DI;Database=bookDb;Integrated Security=SSPI"
    ));
}
else
{
    builder.Services.AddDbContext<Context>(options =>
                    options.UseLazyLoadingProxies().UseSqlServer($"Data Source=mssql-server,1433;Initial Catalog=LojaDB;User Id=SA;Password=Youtube2021;Integrated Security=False;"
    ));
}
var app = builder.Build();

DatabaseService.MigrationInitialisation(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
