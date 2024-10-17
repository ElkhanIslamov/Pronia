using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pronia.Models;

namespace Pronia.Contexts
{
    public class ProniaDbContext : IdentityDbContext<AppUser>
    {
        public ProniaDbContext(DbContextOptions<ProniaDbContext> options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; } = null!;
        public DbSet<Shipping> Shippings { get; set; } = null !;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

    }
}
