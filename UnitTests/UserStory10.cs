using Xunit;

namespace UnitTests
{
  
    public class UserStory10
    {
        // Tests that a bartender can register waste and the product stock decreases.

        [Fact]
        public void Bartender_ShouldBeAbleTo_RegisterWaste()
        {
            var user = new User { Role = UserRole.Bartender };

            var product = new Product
            {
                Name = "Beer Bottle",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new WasteRegistrationService();

            var result = service.RegisterWaste(user, product, amountLost: 2);

            Assert.True(result);
            Assert.Equal(8, product.Stock); 
        }

        // Tests that a non-bartender cannot register waste and stock remains unchanged.

        [Fact]
        public void NonBartender_ShouldNotBeAbleTo_RegisterWaste()
        {
            var user = new User { Role = UserRole.BoardMember };

            var product = new Product
            {
                Name = "Beer Bottle",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new WasteRegistrationService();

            var result = service.RegisterWaste(user, product, amountLost: 2);

            Assert.False(result);
            Assert.Equal(10, product.Stock); 
        }
    }
}
