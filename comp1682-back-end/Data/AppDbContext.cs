using comp1682_back_end.Models;

using Microsoft.EntityFrameworkCore;


namespace comp1682_back_end.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
  }
}
