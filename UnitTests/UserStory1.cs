using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BusinessLogicLayer;

using System;

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

        // Checker at varen formindskes når et product "Købes"
        static bool Test_RegisterSale_ReducesStock_WhenBartenderIsLoggedIn()
        {
            var bartender = new User { IsLoggedIn = true };
            var product = new Product { Name = "Beer", Stock = 10 };
            var inventoryService = new InventoryService();

            inventoryService.RegisterSale(bartender, product);

            return product.Stock == 9;
        }

        // Checker om varen ikke formindskes når et product "Købes"
        static bool Test_RegisterSale_DoesNotReduceStock_WhenBartenderIsNotLoggedIn()
        {
            var bartender = new User { IsLoggedIn = false };
            var product = new Product { Name = "Beer", Stock = 10 };
            var inventoryService = new InventoryService();

            inventoryService.RegisterSale(bartender, product);

            return product.Stock == 10;
        }
    }

    // Klasser til at testen skal kunne fungerr.
    internal class User
    {
        public bool IsLoggedIn { get; set; }
    }

    internal class Product
    {
        public string Name { get; set; }
        public int Stock { get; set; }
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
