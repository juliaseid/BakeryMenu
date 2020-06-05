using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SweetCakes.Models
{
  public class SweetCakesContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Treat> Treats { get; set; }
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<FlavorTreat> FlavorTreat { get; set; }

    public SweetCakesContext(DbContextOptions options) : base(options) { }
  }
}