using Data.Model;
using DataTransferObject.Model;
using Microsoft.EntityFrameworkCore;
using User = Data.Model.User;


namespace Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Data.Model.User> Users { get; set; }
    public DbSet<Data.Model.Product> Products { get; set; }

    public DbSet<Data.Model.Drink> Drink { get; set; }

    public DbSet<Data.Model.DrinkIngredient> DrinkIngredient { get; set; }



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

        base.OnModelCreating(modelBuilder);
    }
}