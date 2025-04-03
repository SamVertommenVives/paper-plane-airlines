using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaperPlaneAirlines.Data;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories;
using PPA.Repositories.Interfaces;
using PPA.Services;
using PPA.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//add PPADbContext
builder.Services.AddDbContext<PPADbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IDAO<City>, CityDAO>();
builder.Services.AddTransient<IFlightDAO, FlightDAO>();
builder.Services.AddTransient<IDAO<Class>, ClassDAO>();

builder.Services.AddTransient<IFlightService, FlightService>();
builder.Services.AddTransient<IService<City>, CityService>();
builder.Services.AddTransient<IService<Class>, ClassService>();

builder.Services.AddDistributedMemoryCache(); // Required for Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Security best practice
    options.Cookie.IsEssential = true; // Ensure session is always stored
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FlightSearch}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();