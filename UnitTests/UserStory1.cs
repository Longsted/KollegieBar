using System;
using BusinessLogicLayer;

namespace UnitTests
{
    internal class UserStory1
    {
        public static void Main()
        {
            Console.WriteLine("Running User Story 1 tests...\n");

            bool test1 = Test_RegisterSale_ReducesStock_WhenBartenderIsLoggedIn();
            Console.WriteLine($"Test 1 - Stock reduces when bartender is logged in: {test1}");

            bool test2 = Test_RegisterSale_DoesNotReduceStock_WhenBartenderIsNotLoggedIn();
            Console.WriteLine($"Test 2 - Stock unchanged when bartender is not logged in: {test2}");
        }

        // Tester at lagerbeholdningen reduceres med 1 når bartenderen er logget ind og registrerer et salg
        static bool Test_RegisterSale_ReducesStock_WhenBartenderIsLoggedIn()
        {
            var bartender = new User { IsLoggedIn = true };
            var product = new Product
            {
                Name = "Beer",
                Stock = 10,
                Type = ProductType.Beer,
                Price = 25.0m
            };

            var inventoryService = new InventoryService();
            inventoryService.RegisterSale(bartender, product);

            return product.Stock == 9;
        }

        // Tester at lagerbeholdningen IKKE reduceres når bartenderen IKKE er logget ind
        static bool Test_RegisterSale_DoesNotReduceStock_WhenBartenderIsNotLoggedIn()
        {
            var bartender = new User { IsLoggedIn = false };
            var product = new Product
            {
                Name = "Beer",
                Stock = 10,
                Type = ProductType.Beer,
                Price = 25.0m
            };

            var inventoryService = new InventoryService();
            inventoryService.RegisterSale(bartender, product);

            return product.Stock == 10;
        }
    }

    internal class User
    {
        public bool IsLoggedIn { get; set; }
    }

    internal enum ProductType
    {
        Beer,
        Cider,
        Soda,
        Spirit
    }

    internal class Product
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public ProductType Type { get; set; }
        public decimal Price { get; set; }
    }

    internal class InventoryService
    {
        public void RegisterSale(User bartender, Product product)
        {
            if (bartender.IsLoggedIn)
                product.Stock -= 1;
        }
    }
}
