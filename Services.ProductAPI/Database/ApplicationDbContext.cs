using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.Models;

namespace Services.ProductAPI.DataBase;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}