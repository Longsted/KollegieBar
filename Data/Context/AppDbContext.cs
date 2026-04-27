using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Drink> Drinks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Register product subtypes
        modelBuilder.Entity<Liquid>();
        modelBuilder.Entity<Snack>();
        modelBuilder.Entity<Consumables>();

        // Many-to-many Drink <-> Liquid
        modelBuilder.Entity<Drink>()
            .HasMany(d => d.Ingredients)
            .WithMany()
            .UsingEntity(j => j.ToTable("DrinkLiquid"));

        // Seed users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, UserName = "Admin", Password = "1234", Role = UserRole.BoardMember },
            new User { Id = 2, UserName = "bar", Password = "1234", Role = UserRole.Bartender }
        );

        // Seed snacks
        modelBuilder.Entity<Snack>().HasData(
            new Snack("Popcorn", 10.00m, 50) { Id = 6 },
            new Snack("Chips", 15.00m, 40) { Id = 7 }
        );

        // Seed liquids
        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Ceres Top", 5.50m, 100, 33, 4.6) { Id = 3 },
            new Liquid("Albani øl", 6.00m, 150, 33, 4.6) { Id = 4 },
            new Liquid("Shaker Sport", 12.00m, 80, 33, 4.5) { Id = 5 }
        );

        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Vodka", 150.00m, 10, 70, 37.5) { Id = 20 },
            new Liquid("Peach Schnapps", 120.00m, 5, 70, 18.0) { Id = 27 },
            new Liquid("Appelsinjuice", 15.00m, 20, 100, false) { Id = 25 },
            new Liquid("Tranebærjuice", 18.00m, 20, 100, false) { Id = 26 }
        );

        // Seed drinks
        modelBuilder.Entity<Drink>().HasData(
            new { Id = 10, Name = "Sex on the beach", CostPrice = 85.00, IsAlcoholic = true },
            new { Id = 11, Name = "Long Island Iced Tea", CostPrice = 95.00, IsAlcoholic = true }
        );

        // No DrinkIngredient seeding anymore

        base.OnModelCreating(modelBuilder);
    }
}
 