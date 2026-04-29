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

        // -----------------------------
        // USERS
        // -----------------------------
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, UserName = "Admin", Password = "1234", Role = UserRole.BoardMember },
            new User { Id = 2, UserName = "bar", Password = "1234", Role = UserRole.Bartender }
        );

        // -----------------------------
        // SNACKS
        // -----------------------------
        modelBuilder.Entity<Snack>().HasData(
            new Snack("Popcorn", 10.00m, 50) { Id = 6 },
            new Snack("Chips", 15.00m, 40) { Id = 7 }
        );

        // -----------------------------
        // LIQUIDS
        // -----------------------------
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

        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Grenadine", 40.00m, 15, 50, 0.0) { Id = 80 },
            new Liquid("Saftevand (Rød)", 15.00m, 50, 100, 0.0) { Id = 81 },
            new Liquid("Ananasjuice", 18.00m, 20, 100, 0.0) { Id = 82 }
        );

        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Råstoff Strawberry/Rhubarb", 130.00m, 10, 70, 16.4) { Id = 41 },
            new Liquid("Jägermeister", 155.00m, 5, 70, 35.0) { Id = 42 },
            new Liquid("Cuba Caramel", 125.00m, 8, 70, 30.0) { Id = 43 },
            new Liquid("Cuba Kurant", 125.00m, 8, 70, 30.0) { Id = 44 },
            new Liquid("Råstoff Liquorice", 130.00m, 10, 70, 16.4) { Id = 45 },
            new Liquid("Southern Comfort", 165.00m, 4, 70, 35.0) { Id = 46 },
            new Liquid("Pisang Ambon", 135.00m, 6, 70, 20.0) { Id = 47 },

            new Liquid("Red Soda", 10.00m, 100, 33, 0.0) { Id = 48 },
            new Liquid("Green Soda", 10.00m, 100, 33, 0.0) { Id = 49 },
            new Liquid("Orange Soda", 10.00m, 100, 33, 0.0) { Id = 55 },
            new Liquid("Chocolate Milk", 12.00m, 40, 100, 0.0) { Id = 56 },
            new Liquid("Lime Juice", 25.00m, 15, 50, 0.0) { Id = 57 },

            new Liquid("Gin", 140.00m, 8, 70, 37.5) { Id = 30 },
            new Liquid("Pink Gin", 145.00m, 5, 70, 37.5) { Id = 31 },
            new Liquid("Mango Syrup", 45.00m, 10, 70, 0.0) { Id = 36 },
            new Liquid("Lemon Soda", 10.00m, 100, 33, 0.0) { Id = 38 },
            new Liquid("Faxe Kondi", 10.00m, 100, 33, 0.0) { Id = 39 },
            new Liquid("Tonic Water", 10.00m, 100, 33, 0.0) { Id = 40 }
        );

        // -----------------------------
        // DRINKS (NO INGREDIENT SEEDING HERE)
        // -----------------------------
        modelBuilder.Entity<Drink>().HasData(
            new { Id = 10, Name = "Sex on the beach", CostPrice = 85.00, IsAlcoholic = true },
            new { Id = 11, Name = "Long Island Iced Tea", CostPrice = 95.00, IsAlcoholic = true },

            new { Id = 60, Name = "Astronaut", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 61, Name = "Basic Bitch", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 62, Name = "Brandbil", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 63, Name = "Champagnebrus", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 64, Name = "Purple Rain", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 65, Name = "Dumle", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 66, Name = "Filur", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 67, Name = "Flagermus", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 68, Name = "Gin Hass", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 69, Name = "Skumbanan", CostPrice = 20.0, IsAlcoholic = true },
            new { Id = 70, Name = "Southern & Sprite", CostPrice = 20.0, IsAlcoholic = true }
        );

        modelBuilder.Entity<Drink>().HasData(
            new { Id = 90, Name = "Børnebrandbil", CostPrice = 20.0, IsAlcoholic = false },
            new { Id = 91, Name = "Børnechampagnebrus", CostPrice = 20.0, IsAlcoholic = false },
            new { Id = 92, Name = "Børnefilur", CostPrice = 20.0, IsAlcoholic = false },
            new { Id = 93, Name = "Børneastronaut", CostPrice = 20.0, IsAlcoholic = false },
            new { Id = 94, Name = "Safe Sex On The Beach", CostPrice = 20.0, IsAlcoholic = false }
        );

        base.OnModelCreating(modelBuilder);
    }
}
