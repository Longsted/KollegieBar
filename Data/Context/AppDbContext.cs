using Data.Model;
using Microsoft.EntityFrameworkCore;


namespace Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Data.Model.User> Users { get; set; }
    public DbSet<Data.Model.Product> Products { get; set; }
    public DbSet<Data.Model.Sale> Sales { get; set; }
    public DbSet<Data.Model.Drink> Drinks { get; set; }
    public DbSet<Data.Model.DrinkIngredient> DrinkIngredients { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Data.Model.Liquid>();
        modelBuilder.Entity<Data.Model.Snack>();
        modelBuilder.Entity<Data.Model.Consumables>();

        //seed users
        modelBuilder.Entity<Data.Model.User>().HasData(
            new Data.Model.User
            {
                Id = 1,
                UserName = "Admin",
                Password = "1234",
                Role = UserRole.BoardMember
            }, new Data.Model.User
            {
                Id = 2,
                UserName = "bar",
                Password = "1234",
                Role = UserRole.Bartender
            }
        );
        
        modelBuilder.Entity<Snack>().HasData(
    new Snack("Popcorn", 10.00m, 50)
    {
        Id = 6
    },
    new Snack("Chips", 15.00m, 40)
    {
        Id = 7
    }
); modelBuilder.Entity<Liquid>().HasData(
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

        modelBuilder.Entity<Drink>().HasData(
        new { Id = 10, Name = "Sex on the beach", CostPrice = 85.00, IsAlcoholic = true },
        new { Id = 11, Name = "Long Island Iced Tea", CostPrice = 95.00, IsAlcoholic = true }
    );

        

        // 3. Seed DrinkIngredients - HER ER FIXET
        // Vi tilføjer både LiquidProductId og LiquidId for at stoppe fejlen
        modelBuilder.Entity<DrinkIngredient>().HasData(
            new { Id = 1, DrinkId = 10, LiquidProductId = 20, LiquidId = 20, VolumeCl = 4 },
            new { Id = 2, DrinkId = 10, LiquidProductId = 27, LiquidId = 27, VolumeCl = 2 },
            new { Id = 3, DrinkId = 10, LiquidProductId = 25, LiquidId = 25, VolumeCl = 6 },
            new { Id = 4, DrinkId = 10, LiquidProductId = 26, LiquidId = 26, VolumeCl = 6 }
        );

        base.OnModelCreating(modelBuilder);
    }
}