using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SweetCakes.Models
{
  public class SweetCakesContextFactory : IDesignTimeDbContextFactory<SweetCakesContext>
  {

    SweetCakesContext IDesignTimeDbContextFactory<SweetCakesContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<SweetCakesContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new SweetCakesContext(builder.Options);
    }
  }
}