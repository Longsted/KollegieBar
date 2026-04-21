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
        // Registrer dine konkrete typer
        modelBuilder.Entity<Data.Model.LiquidWithAlcohol>();
        modelBuilder.Entity<Data.Model.LiquidWithoutAlcohol>();
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
        modelBuilder.Entity<LiquidWithAlcohol>().HasData(
    new LiquidWithAlcohol("Ceres Top", 5.50m, 100, 33, 20.00m, 4.6)
    {
        Id = 3 
    },
    new LiquidWithAlcohol("Albani øl", 6.00m, 150, 33, 22.00m, 4.6)
    {
        Id = 4
    },
    new LiquidWithAlcohol("Shaker Sport", 12.00m, 80, 33, 35.00m, 4.5)
    {
        Id = 5
    }
);

        base.OnModelCreating(modelBuilder);
    }
}