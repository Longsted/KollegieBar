using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class UserStory5
    {
        // Tests that a board member receives a list of low inventory items when logged in.
        [Fact]
        public void BoardMember_ShouldReceive_ListOfLowInventoryItems()
        {
            var user = new User { Role = UserRole.BoardMember };

            var products = new List<Product>
            {
                new Product { Name = "Beer", Stock = 2, Type = DrinkType.Beer, Price = 10 },
                new Product { Name = "Cider", Stock = 10, Type = DrinkType.Cider, Price = 12 },
                new Product { Name = "Soda", Stock = 1, Type = DrinkType.Soda, Price = 8 },
                new Product { Name = "Spirit", Stock = 20, Type = DrinkType.Spirit, Price = 50 }
            };

            int threshold = 5;
            var service = new LowInventoryService(products, threshold);

            var lowInventory = service.GetLowInventory(user);

            Assert.NotNull(lowInventory);
            Assert.Equal(2, lowInventory.Count);

            Assert.Contains(lowInventory, p => p.Name == "Beer");
            Assert.Contains(lowInventory, p => p.Name == "Soda");

            Assert.DoesNotContain(lowInventory, p => p.Name == "Cider");
            Assert.DoesNotContain(lowInventory, p => p.Name == "Spirit");
        }

        // Tests that a non-board member receives an empty list when requesting low inventory items.
        [Fact]
        public void NonBoardMember_ShouldReceive_EmptyList()
        {
            var user = new User { Role = UserRole.Bartender };

            var products = new List<Product>
                {
                new Product { Name = "Beer", Stock = 1, Type = DrinkType.Beer, Price = 10 }
                };

            var service = new LowInventoryService(products, 5);

            var result = service.GetLowInventory(user);

            Assert.Empty(result); 
        }

    }
}
