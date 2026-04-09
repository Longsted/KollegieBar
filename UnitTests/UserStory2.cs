using Xunit;

namespace UnitTests
{
    public class UserStory1Tests
    {
        // Tester at lagerbeholdningen reduceres med 1 når bartenderen registrerer et salg
        [Fact]
        public void RegisterSale_ReducesStock_WhenUserRoleIsBartender()
        {
            var bartender = new User { Role = UserRole.Bartender };
            var product = new Product
            {
                Name = "Beer",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 25.0m
            };

            var inventoryService = new InventoryService();
            inventoryService.RegisterSale(product);

            Assert.Equal(9, product.Stock);
        }

        // Tester at lagerbeholdningen IKKE reduceres når rollen er null (ikke logget ind)
        [Fact]
        public void RegisterSale_DoesNotReduceStock_WhenUserRoleIsNull()
        {
            var noRoleUser = new User { Role = null };
            var product = new Product
            {
                Name = "Beer",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 25.0m
            };

            var inventoryService = new InventoryService();
            inventoryService.RegisterSale(product);

            Assert.Equal(10, product.Stock);
        }
    }
}
