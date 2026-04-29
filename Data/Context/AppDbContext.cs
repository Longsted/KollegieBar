using Data.Model;
using DataTransferObject.Model;
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
        modelBuilder.Entity<Liquid>().HasData(
    new Liquid("Grenadine", 40.00m, 15, 50, 0.0) { Id = 80 },
    new Liquid("Saftevand (Rød)", 15.00m, 50, 100, 0.0) { Id = 81 },
    new Liquid("Ananasjuice", 18.00m, 20, 100, 0.0) { Id = 82 }
);
        modelBuilder.Entity<Liquid>().HasData(
    // Spiritus & Likør
    new Liquid("Råstoff Strawberry/Rhubarb", 130.00m, 10, 70, 16.4) { Id = 41 },
    new Liquid("Jägermeister", 155.00m, 5, 70, 35.0) { Id = 42 },
    new Liquid("Cuba Caramel", 125.00m, 8, 70, 30.0) { Id = 43 },
    new Liquid("Cuba Kurant", 125.00m, 8, 70, 30.0) { Id = 44 },
    new Liquid("Råstoff Liquorice", 130.00m, 10, 70, 16.4) { Id = 45 },
    new Liquid("Southern Comfort", 165.00m, 4, 70, 35.0) { Id = 46 },
    new Liquid("Pisang Ambon", 135.00m, 6, 70, 20.0) { Id = 47 },

    // Mixere
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


        modelBuilder.Entity<DrinkIngredient>().HasData(
            new { Id = 1, DrinkId = 10, LiquidProductId = 20, LiquidId = 20},
            new { Id = 2, DrinkId = 10, LiquidProductId = 27, LiquidId = 27},
            new { Id = 3, DrinkId = 10, LiquidProductId = 25, LiquidId = 25},
            new { Id = 4, DrinkId = 10, LiquidProductId = 26, LiquidId = 26},
        // Astronaut: Råstoff Strawberry (41), Lemon Soda (38)
    new { Id = 30, DrinkId = 60, LiquidProductId = 41, LiquidId = 41 },
    new { Id = 31, DrinkId = 60, LiquidProductId = 38, LiquidId = 38 },

    // Basic Bitch: Pink Gin (31), Lime Juice (57), Lemon Soda (38), Tranebærjuice (26)
    new { Id = 32, DrinkId = 61, LiquidProductId = 31, LiquidId = 31 },
    new { Id = 33, DrinkId = 61, LiquidProductId = 57, LiquidId = 57 },
    new { Id = 34, DrinkId = 61, LiquidProductId = 38, LiquidId = 38 },
    new { Id = 35, DrinkId = 61, LiquidProductId = 26, LiquidId = 26 },

    // Brandbil: Jägermeister (42), Red Soda (48)
    new { Id = 36, DrinkId = 62, LiquidProductId = 42, LiquidId = 42 },
    new { Id = 37, DrinkId = 62, LiquidProductId = 48, LiquidId = 48 },

    // Champagnebrus: Cuba Caramel (43), Green Soda (49)
    new { Id = 38, DrinkId = 63, LiquidProductId = 43, LiquidId = 43 },
    new { Id = 39, DrinkId = 63, LiquidProductId = 49, LiquidId = 49 },

    // Purple Rain: Cuba Kurant (44), Faxe Kondi (39), Lime Juice (57)
    new { Id = 40, DrinkId = 64, LiquidProductId = 44, LiquidId = 44 },
    new { Id = 41, DrinkId = 64, LiquidProductId = 39, LiquidId = 39 },
    new { Id = 42, DrinkId = 64, LiquidProductId = 57, LiquidId = 57 },

    // Dumle: Cuba Caramel (43), Chocolate Milk (56)
    new { Id = 43, DrinkId = 65, LiquidProductId = 43, LiquidId = 43 },
    new { Id = 44, DrinkId = 65, LiquidProductId = 56, LiquidId = 56 },

    // Filur: Vodka (20), Red Soda (48), Appelsinjuice (25)
    new { Id = 45, DrinkId = 66, LiquidProductId = 20, LiquidId = 20 },
    new { Id = 46, DrinkId = 66, LiquidProductId = 48, LiquidId = 48 },
    new { Id = 47, DrinkId = 66, LiquidProductId = 25, LiquidId = 25 },

    // Flagermus: Råstoff Liquorice (45), Råstoff Strawberry (41), Orange Soda (55)
    new { Id = 48, DrinkId = 67, LiquidProductId = 45, LiquidId = 45 },
    new { Id = 49, DrinkId = 67, LiquidProductId = 41, LiquidId = 41 },
    new { Id = 50, DrinkId = 67, LiquidProductId = 55, LiquidId = 55 },

    // Gin Hass: Gin (30), Mango Syrup (36), Lemon Soda (38)
    new { Id = 51, DrinkId = 68, LiquidProductId = 30, LiquidId = 30 },
    new { Id = 52, DrinkId = 68, LiquidProductId = 36, LiquidId = 36 },
    new { Id = 53, DrinkId = 68, LiquidProductId = 38, LiquidId = 38 },

    // Skumbanan: Pisang Ambon (47), Chocolate Milk (56)
    new { Id = 54, DrinkId = 69, LiquidProductId = 47, LiquidId = 47 },
    new { Id = 55, DrinkId = 69, LiquidProductId = 56, LiquidId = 56 },

    // Southern & Sprite: Southern Comfort (46), Faxe Kondi (39)
    new { Id = 56, DrinkId = 70, LiquidProductId = 46, LiquidId = 46 },
    new { Id = 57, DrinkId = 70, LiquidProductId = 39, LiquidId = 39 }
);
        modelBuilder.Entity<DrinkIngredient>().HasData(
    // Børnebrandbil: Saftevand (81), Red Soda (48)
    new { Id = 100, DrinkId = 90, LiquidProductId = 81, LiquidId = 81 },
    new { Id = 101, DrinkId = 90, LiquidProductId = 48, LiquidId = 48 },

    // Børnechampagnebrus: Saftevand (81), Green Soda (49)
    new { Id = 102, DrinkId = 91, LiquidProductId = 81, LiquidId = 81 },
    new { Id = 103, DrinkId = 91, LiquidProductId = 49, LiquidId = 49 },

    // Børnefilur: Appelsinjuice (25), Red Soda (48)
    new { Id = 104, DrinkId = 92, LiquidProductId = 25, LiquidId = 25 },
    new { Id = 105, DrinkId = 92, LiquidProductId = 48, LiquidId = 48 },

    // Børneastronaut: Saftevand (81), Lemon Soda (38)
    new { Id = 106, DrinkId = 93, LiquidProductId = 81, LiquidId = 81 },
    new { Id = 107, DrinkId = 93, LiquidProductId = 38, LiquidId = 38 },

    // Safe Sex On The Beach: Appelsinjuice (25), Tranebærjuice (26), Ananasjuice (82), Grenadine (80)
    new { Id = 108, DrinkId = 94, LiquidProductId = 25, LiquidId = 25 },
    new { Id = 109, DrinkId = 94, LiquidProductId = 26, LiquidId = 26 },
    new { Id = 110, DrinkId = 94, LiquidProductId = 82, LiquidId = 82 },
    new { Id = 111, DrinkId = 94, LiquidProductId = 80, LiquidId = 80 }
);

        base.OnModelCreating(modelBuilder);
    }
}