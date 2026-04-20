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

        base.OnModelCreating(modelBuilder);
    }
}