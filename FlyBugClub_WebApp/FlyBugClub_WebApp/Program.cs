using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;
using FlyBugClub_WebApp.Data;
using FlyBugClub_WebApp.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection
builder.Services.AddDbContext<FlyBugClubWebApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlyBugDBContext"));
});

builder.Services.AddDbContext<FlyBugClubDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlyBugClubDB"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/LoginCustomer";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false) //tắt confirm email
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FlyBugClubDBContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});

//DI
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IGenreRepository, GenreProductRepository>();
builder.Services.AddTransient<IOrderProcessingRepository, OrderProcessingRepository>();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews()
               .AddRazorPagesOptions(
         options => options.Conventions.AddAreaPageRoute("Identity", "/Account/LoginCustomer", "/Account/Login/LoginCustomer"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Admin",
    pattern: "Admin/{controller}/{action}/{id?}",
    defaults: new { controller = "Dashboard", action = "Dashboard" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Store}/{id?}");

app.MapRazorPages();
app.Run();

/*Scaffold-DBContext "Server=.\SQLEXPRESS;uid=sa;password=1;database=FlyBugClub_WebApplication;Encrypt=true;TrustServerCertificate=true" Microsoft.EntityFrameWorkCore.SqlServer -OutputDir Models*/
