using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaperPlaneAirlines.Controllers;
using PaperPlaneAirlines.Data;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories;
using PPA.Repositories.Interfaces;
using PPA.Services;
using PPA.Services.Interfaces;
using SendMail.Util;
using SendMail.Util.Interfaces;

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

builder.Services.AddScoped<IDAO<City>, CityDAO>();
builder.Services.AddScoped<IFlightDAO, FlightDAO>();
builder.Services.AddScoped<IDAO<Class>, ClassDAO>();
builder.Services.AddScoped<IMenuDAO, MenuDAO>();
builder.Services.AddScoped<IFlightRouteDAO, FlightRouteDAO>();
builder.Services.AddScoped<IDAO<Booking>, BookingDAO>();
builder.Services.AddScoped<IFlightBookingDAO, FlightBookingDAO>();

builder.Services.AddScoped<IService<City>, CityService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IService<Class>, ClassService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IFlightRouteService, FlightRouteService>();
builder.Services.AddScoped<IBookingOptionService, BookingOptionService>();
builder.Services.AddScoped<IService<Booking>, BookingService>();
builder.Services.AddScoped<IFlightBookingService, FlightBookingService>();
//builder.Services.AddTransient<IService<Discount>, DiscountService>();

builder.Services.AddScoped<IHotelService, HotelService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//add HttpClientFactory
builder.Services.AddHttpClient();

//add Email Service
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IEmailSend, EmailSend>();
builder.Services.AddScoped<SendController>();


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

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FlightSearch}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();