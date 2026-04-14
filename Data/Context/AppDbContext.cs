using DataTransferObject.Model;
using Microsoft.EntityFrameworkCore;
using User = Data.Model.User;

namespace Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { 
    }
}