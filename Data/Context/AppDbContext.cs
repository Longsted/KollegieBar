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
            new Snack("Popcorn", 7.00m, 4) { Id = 1 },
            new Snack("Chips", 10.00m, 4) { Id = 2 }
        );

        // -----------------------------
        // LIQUIDS
        // -----------------------------

        // Beer
        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Ceres Top", 7.00m, 100, 33, 4.6) { Id = 3, Pant = Pant.A },
            new Liquid("Albani Odense Classic", 7.00m, 100, 33, 4.6) { Id = 4, Pant = Pant.A },
            new Liquid("Royal Pilsner (0%)", 7.00m, 10, 33, 0.0) { Id = 5, Pant = Pant.A },
            new Liquid("Shaker", 10.00m, 10, 33, 4.5) { Id = 6, Pant = Pant.A }
        );

        // Liquor
        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Vodka", 90.00m, 10, 70, 37.5) { Id = 20 },
            new Liquid("Gin", 110.00m, 8, 70, 37.5) { Id = 30 },
            new Liquid("Licor 43", 120.00m, 5, 50, 31.0) { Id = 48 },
            new Liquid("Kahlua", 110.00m, 5, 70, 20.0) { Id = 35 },
            new Liquid("Jägermeister", 155.00m, 5, 70, 35.0) { Id = 42 },
            new Liquid("Cuba Caramel", 90.00m, 8, 70, 30.0) { Id = 43 },
            new Liquid("Cuba Kurant", 90.00m, 8, 70, 30.0) { Id = 44 },
            new Liquid("Rum", 150.00m, 10, 70, 37.5) { Id = 32 },
            new Liquid("Tequila", 160.00m, 5, 70, 38.0) { Id = 33 },
            new Liquid("Cointreau", 180.00m, 5, 70, 40.0) { Id = 34 },
            new Liquid("Pisang Ambon", 135.00m, 6, 70, 20.0) { Id = 47 },
            new Liquid("Southern Comfort", 165.00m, 4, 70, 35.0) { Id = 46 },
            new Liquid("Råstoff Strawberry/Rhubarb", 130.00m, 10, 70, 16.4) { Id = 41 },
            new Liquid("Råstoff Liquorice", 130.00m, 10, 70, 16.4) { Id = 45 },
            new Liquid("Råstoff Pineapple/Vanilla", 130.00m, 5, 70, 16.4) { Id = 49 },
            new Liquid("Pink Gin", 110.00m, 5, 70, 37.5) { Id = 31 },
            new Liquid("Non-Alcoholic Gin", 120.00m, 5, 70, 0.0) { Id = 100 }
        );

        // Soda, Tonic and Energydrink
        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Faxe Kondi", 10.00m, 100, 33, false) { Id = 39, Pant = Pant.A },
            new Liquid("Faxe Kondi Free", 10.00m, 100, 33, true) { Id = 40, Pant = Pant.A },
            new Liquid("Pepsi", 10.00m, 100, 33, false) { Id = 50, Pant = Pant.A },
            new Liquid("Pepsi Max", 10.00m, 100, 33, true) { Id = 51, Pant = Pant.A },
            new Liquid("Lemon Soda", 10.00m, 100, 33, false) { Id = 38, Pant = Pant.A },
            new Liquid("Red Soda", 10.00m, 100, 33, false) { Id = 52, Pant = Pant.A },
            new Liquid("Green Soda", 10.00m, 100, 33, false) { Id = 53, Pant = Pant.A },
            new Liquid("Orange Soda", 10.00m, 100, 33, false) { Id = 54, Pant = Pant.A },
            new Liquid("Energy Drink", 12.00m, 20, 50, true) { Id = 55, Pant = Pant.A },
            new Liquid("Tonic Water", 10.00m, 10, 33, false) { Id = 76, Pant = Pant.A }
        );

        // Mixer and Juice
        modelBuilder.Entity<Liquid>().HasData(
            new Liquid("Orange Juice", 10.00m, 6, 100, false) { Id = 25 },
            new Liquid("Cranberry Juice", 10.00m, 5, 100, false) { Id = 26 },
            new Liquid("Chocolate Milk", 10.00m, 5, 100, false) { Id = 56 },
            new Liquid("Lime Juice", 6.00m, 7, 50, false) { Id = 59 },
            new Liquid("Lemon Juice", 6.00m, 2, 50, false) { Id = 83 },

            new Liquid("Mango Syrup", 45.00m, 1, 70, false) { Id = 57 },
            new Liquid("Passion Syrup", 45.00m, 1, 70, false) { Id = 58 },
            new Liquid("Grenadine", 40.00m, 1, 70, false) { Id = 80 },
            new Liquid("Lemon Syrup", 30.00m, 1, 70, false) { Id = 81 },
            new Liquid("Blue Curacao", 120.00m, 5, 70, false) { Id = 75 }
        );

        // -----------------------------
        // DRINKS
        // -----------------------------
        modelBuilder.Entity<Drink>().HasData(
            // 20-25 kr. Cocktails
            new { Id = 60, Name = "Astronaut", IsAlcoholic = true, Description = "Råstoff Strawberry/Rhubarb, Lemon Soda", IsCustom = false },
            new { Id = 61, Name = "Basic Bitch", IsAlcoholic = true, Description = "Pink Gin, Lime Juice, Lemon Soda, Cranberry Juice", IsCustom = false },
            new { Id = 62, Name = "Brandbil", IsAlcoholic = true, Description = "Jägermeister, Red Soda", IsCustom = false },
            new { Id = 63, Name = "Champagnebrus", IsAlcoholic = true, Description = "Cuba Caramel, Green Soda", IsCustom = false },
            new { Id = 64, Name = "Purple Rain", IsAlcoholic = true, Description = "Cuba Kurant, Faxe Kondi, Lime Juice", IsCustom = false },
            new { Id = 65, Name = "Dumle", IsAlcoholic = true, Description = "Cuba Caramel, Chocolate Milk", IsCustom = false },
            new { Id = 66, Name = "Filur", IsAlcoholic = true, Description = "Vodka, Red Soda, Orange Juice", IsCustom = false },
            new { Id = 67, Name = "Flagermus", IsAlcoholic = true, Description = "Råstoff Liquorice, Råstoff Strawberry/Rhubarb, Orange Soda", IsCustom = false },
            new { Id = 68, Name = "Gin Hass", IsAlcoholic = true, Description = "Gin, Mango Syrup, Lemon Soda", IsCustom = false },
            new { Id = 69, Name = "Gin & Tonic/Lemon", IsAlcoholic = true, Description = "Gin/Pink Gin, Tonic Water/Lemon Soda", IsCustom = false },
            new { Id = 70, Name = "Isbjørn", IsAlcoholic = true, Description = "Vodka, Blue Curacao, Faxe Kondi", IsCustom = false },
            new { Id = 71, Name = "Sex On The Beach", IsAlcoholic = true, Description = "Vodka, Mango Syrup, Cranberry Juice, Orange Juice", IsCustom = false },
            new { Id = 72, Name = "Skumbanan", IsAlcoholic = true, Description = "Pisang Ambon, Chocolate Milk", IsCustom = false },
            new { Id = 73, Name = "Southern & Sprite", IsAlcoholic = true, Description = "Southern Comfort, Lime Syrup, Faxe Kondi", IsCustom = false },
            new { Id = 74, Name = "Rum & Coke", IsAlcoholic = true, Description = "Rum, Pepsi/Max", IsCustom = false },
            new { Id = 75, Name = "Tequila Sunrise", IsAlcoholic = true, Description = "Tequila, Grenadine, Orange Juice", IsCustom = false },
            new { Id = 76, Name = "White Russian", IsAlcoholic = true, Description = "Kahlua, Vodka, Milk/Chocolate Milk", IsCustom = false },
            new { Id = 77, Name = "Chocolate & Bailey", IsAlcoholic = true, Description = "Bailey, Chocolate Milk", IsCustom = false },
            new { Id = 78, Name = "Green Goblin", IsAlcoholic = true, Description = "Pisang Ambon, Orange Juice", IsCustom = false },
            new { Id = 79, Name = "Cosmopolitan", IsAlcoholic = true, Description = "Cointreau, Vodka, Cranberry Juice, Lime Juice", IsCustom = false },
            new { Id = 80, Name = "Shaker Jäger", IsAlcoholic = true, Description = "Jägermeister, Shaker", IsCustom = false },

            // 30 kr. Drinks
            new { Id = 81, Name = "Vodka Energy", IsAlcoholic = true, Description = "Vodka, Energy Drink", IsCustom = false },
            new { Id = 82, Name = "3-Meter-Vippen", IsAlcoholic = true, Description = "Southern Comfort, Vodka, Lemon, Passion Syrup, Faxe Kondi", IsCustom = false },
            new { Id = 83, Name = "Exotic", IsAlcoholic = true, Description = "Cointreau, Vodka, Passion, Mango Syrup, Grenadine, Faxe Kondi", IsCustom = false },
            new { Id = 84, Name = "Long Island Iced Tea", IsAlcoholic = true, Description = "Cointreau, Gin, Rum, Tequila, Vodka, Lemon Juice, Pepsi/Max", IsCustom = false },
            new { Id = 85, Name = "Lille Fugl Fald Død Om", IsAlcoholic = true, Description = "Gin, Pisang Ambon, Rum, Vodka, Lemon Juice, Lemon Syrup, Faxe Kondi", IsCustom = false }
        );

        // Mocktails
        modelBuilder.Entity<Drink>().HasData(
            new { Id = 90, Name = "Boring Bitch", IsAlcoholic = false, Description = "Non-Alcoholic Gin, Lemon Soda, Cranberry Juice, Lime Juice", IsCustom = false },
            new { Id = 91, Name = "Filur Free", IsAlcoholic = false, Description = "Non-Alcoholic Gin, Red Soda, Orange Juice", IsCustom = false },
            new { Id = 92, Name = "Gin Love", IsAlcoholic = false, Description = "Non-Alcoholic Gin, Mango Syrup, Lemon Soda", IsCustom = false },
            new { Id = 93, Name = "Panda", IsAlcoholic = false, Description = "Non-Alcoholic Gin, Blue Curacao, Faxe Kondi/Free", IsCustom = false },
            new { Id = 94, Name = "(Levende) Panda", IsAlcoholic = false, Description = "Non-Alcoholic Gin, Blue Curacao, Faxe Kondi/Free (+ Grenadine)", IsCustom = false },
            new { Id = 95, Name = "Virgin Sex", IsAlcoholic = false, Description = "Non-Alcoholic Gin, Mango Syrup, Cranberry Juice, Orange Juice", IsCustom = false },
            new { Id = 96, Name = "Gin Sunset", IsAlcoholic = false, Description = "Non-Alcoholic Gin, Grenadine, Orange Juice", IsCustom = false }
        );

        modelBuilder.Entity("DrinkLiquid").HasData(
            // Astronaut: Råstoff Strawberry (41), Lemon Soda (38)
            new { DrinkId = 60, IngredientsId = 41 },
            new { DrinkId = 60, IngredientsId = 38 },

            // Basic Bitch: Pink Gin (31), Lime Juice (59), Lemon Soda (38), Cranberry Juice (26)
            new { DrinkId = 61, IngredientsId = 31 },
            new { DrinkId = 61, IngredientsId = 59 },
            new { DrinkId = 61, IngredientsId = 38 },
            new { DrinkId = 61, IngredientsId = 26 },

            // Brandbil: Jägermeister (42), Red Soda (52)
            new { DrinkId = 62, IngredientsId = 42 },
            new { DrinkId = 62, IngredientsId = 52 },

            // Champagnebrus: Cuba Caramel (43), Green Soda (53)
            new { DrinkId = 63, IngredientsId = 43 },
            new { DrinkId = 63, IngredientsId = 53 },

            // Purple Rain: Cuba Kurant (44), Faxe Kondi (39), Lime Juice (59)
            new { DrinkId = 64, IngredientsId = 44 },
            new { DrinkId = 64, IngredientsId = 39 },
            new { DrinkId = 64, IngredientsId = 59 },

            // Dumle: Cuba Caramel (43), Chocolate Milk (56)
            new { DrinkId = 65, IngredientsId = 43 },
            new { DrinkId = 65, IngredientsId = 56 },

            // Filur: Vodka (20), Red Soda (52), Orange Juice (25)
            new { DrinkId = 66, IngredientsId = 20 },
            new { DrinkId = 66, IngredientsId = 52 },
            new { DrinkId = 66, IngredientsId = 25 },

            // Flagermus: Råstoff Liquorice (45), Råstoff Strawberry (41), Orange Soda (54)
            new { DrinkId = 67, IngredientsId = 45 },
            new { DrinkId = 67, IngredientsId = 41 },
            new { DrinkId = 67, IngredientsId = 54 },

            // Gin Hass: Gin (30), Mango Syrup (57), Lemon Soda (38)
            new { DrinkId = 68, IngredientsId = 30 },
            new { DrinkId = 68, IngredientsId = 57 },
            new { DrinkId = 68, IngredientsId = 38 },

            // Skumbanan: Pisang Ambon (47), Chocolate Milk (56)
            new { DrinkId = 72, IngredientsId = 47 },
            new { DrinkId = 72, IngredientsId = 56 },

            // Isbjørn: Vodka (20), Blue Curacao (75), Faxe Kondi (39)
            new { DrinkId = 70, IngredientsId = 20 },
            new { DrinkId = 70, IngredientsId = 75 },
            new { DrinkId = 70, IngredientsId = 39 },

            // Gin & Tonic: Gin (30), Tonic Water (76)
            new { DrinkId = 69, IngredientsId = 30 },
            new { DrinkId = 69, IngredientsId = 76 },

            // Rum & Coke: Rum (32), Pepsi (50)
            new { DrinkId = 74, IngredientsId = 32 },
            new { DrinkId = 74, IngredientsId = 50 },

            // Shaker Jäger: Jägermeister (42), Shaker (6)
            new { DrinkId = 80, IngredientsId = 42 },
            new { DrinkId = 80, IngredientsId = 55 },

            // Vodka Energy: Vodka (20), Shaker (6)
            new { DrinkId = 81, IngredientsId = 20 },
            new { DrinkId = 81, IngredientsId = 6 },

            // Southern & Sprite: Southern Comfort (46), Lemon Syrup (81), Faxe Kondi (39)
            new { DrinkId = 73, IngredientsId = 46 },
            new { DrinkId = 73, IngredientsId = 81 },
            new { DrinkId = 73, IngredientsId = 39 },

            // 3-Meter-Vippen: Southern Comfort (46), Vodka (20), Lemon Syrup (81), Passion Syrup (58), Faxe Kondi (39)
            new { DrinkId = 82, IngredientsId = 46 },
            new { DrinkId = 82, IngredientsId = 20 },
            new { DrinkId = 82, IngredientsId = 81 },
            new { DrinkId = 82, IngredientsId = 58 },
            new { DrinkId = 82, IngredientsId = 39 },

            // Exotic: Cointreau (34), Vodka (20), Passion Syrup (58), Mango Syrup (57), Grenadine (80), Faxe Kondi (39)
            new { DrinkId = 83, IngredientsId = 34 },
            new { DrinkId = 83, IngredientsId = 20 },
            new { DrinkId = 83, IngredientsId = 58 },
            new { DrinkId = 83, IngredientsId = 57 },
            new { DrinkId = 83, IngredientsId = 80 },
            new { DrinkId = 83, IngredientsId = 39 },

            // --- PREMIUM DRINKS ---
            // Long Island Iced Tea: Cointreau (34), Gin (30), Rum (32), Tequila (33), Vodka (20), Pepsi (50), Lime Juice (59)
            new { DrinkId = 84, IngredientsId = 34 },
            new { DrinkId = 84, IngredientsId = 30 },
            new { DrinkId = 84, IngredientsId = 32 },
            new { DrinkId = 84, IngredientsId = 33 },
            new { DrinkId = 84, IngredientsId = 20 },
            new { DrinkId = 84, IngredientsId = 50 },
            new { DrinkId = 84, IngredientsId = 59 },

            // Lille Fugl Fald Død Om: Gin (30), Pisang Ambon (47), Rum (32), Vodka (20), Lime Juice (59), Faxe Kondi (39)
            new { DrinkId = 85, IngredientsId = 30 },
            new { DrinkId = 85, IngredientsId = 47 },
            new { DrinkId = 85, IngredientsId = 32 },
            new { DrinkId = 85, IngredientsId = 20 },
            new { DrinkId = 85, IngredientsId = 59 },
            new { DrinkId = 85, IngredientsId = 39 },

            // --- MOCKTAILS ---
            // Boring Bitch: Non-Alcoholic Gin (100), Lemon Soda (38), Cranberry Juice (26), Lime Juice (59)
            new { DrinkId = 90, IngredientsId = 100 },
            new { DrinkId = 90, IngredientsId = 38 },
            new { DrinkId = 90, IngredientsId = 26 },
            new { DrinkId = 90, IngredientsId = 59 },

            // Filur Free: Non-Alcoholic Gin (100), Red Soda (52), Orange Juice (25)
            new { DrinkId = 91, IngredientsId = 100 },
            new { DrinkId = 91, IngredientsId = 52 },
            new { DrinkId = 91, IngredientsId = 25 },

            // Gin Love: Non-Alcoholic Gin (100), Mango Syrup (57), Lemon Soda (38)
            new { DrinkId = 92, IngredientsId = 100 },
            new { DrinkId = 92, IngredientsId = 57 },
            new { DrinkId = 92, IngredientsId = 38 },

            // Panda: Non-Alcoholic Gin (100), Blue Curacao (75), Faxe Kondi (39)
            new { DrinkId = 93, IngredientsId = 100 },
            new { DrinkId = 93, IngredientsId = 75 },
            new { DrinkId = 93, IngredientsId = 39 },

            // (Levende) Panda: Non-Alcoholic Gin (100), Blue Curacao (75), Faxe Kondi (39), Grenadine (80)
            new { DrinkId = 94, IngredientsId = 100 },
            new { DrinkId = 94, IngredientsId = 75 },
            new { DrinkId = 94, IngredientsId = 39 },
            new { DrinkId = 94, IngredientsId = 80 },

            // Virgin Sex: Non-Alc Gin (100), Mango Syrup (57), Cranberry Juice (26), Orange Juice (25)
            new { DrinkId = 95, IngredientsId = 100 },
            new { DrinkId = 95, IngredientsId = 57 },
            new { DrinkId = 95, IngredientsId = 26 },
            new { DrinkId = 95, IngredientsId = 25 },

            // Gin Sunset: Non-Alc Gin (100), Grenadine (80), Orange Juice (25)
            new { DrinkId = 96, IngredientsId = 100 },
            new { DrinkId = 96, IngredientsId = 80 },
            new { DrinkId = 96, IngredientsId = 25 }
        );

        base.OnModelCreating(modelBuilder);
    }
}