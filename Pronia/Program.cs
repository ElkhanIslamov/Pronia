using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;
using Pronia.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProniaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    options.Lockout.AllowedForNewUsers = true;
<<<<<<< HEAD
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
=======
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    options.Lockout.MaxFailedAccessAttempts = 3;
})
.AddEntityFrameworkStores<ProniaDbContext>()
.AddDefaultTokenProviders();

<<<<<<< HEAD
var app = builder.Build();

app.UseAuthentication();    
app.UseAuthorization();

=======


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"
    );

app.UseStaticFiles();



app.Run();
