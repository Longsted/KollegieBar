using DataTransferObject.Model;
using Microsoft.EntityFrameworkCore;
using User = Data.Model.User;


namespace Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Data.Model.User> Users { get; set; }
    public DbSet<Data.Model.Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Registrer dine konkrete typer
        modelBuilder.Entity<Data.Model.LiquidWithAlcohol>();
        modelBuilder.Entity<Data.Model.LiquidWithoutAlcohol>();
        modelBuilder.Entity<Data.Model.Snack>();
        modelBuilder.Entity<Data.Model.Consumables>();

        base.OnModelCreating(modelBuilder);
    }
}