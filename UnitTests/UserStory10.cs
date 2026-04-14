using Xunit;

namespace UnitTests
{
    // Tests that a bartender can register waste and the product stock decreases.
    // Tests that a non-bartender cannot register waste and stock remains unchanged.

    public class UserStory10
    {
        [Fact]
        public void Bartender_ShouldBeAbleTo_RegisterWaste()
        {
            // Arrange
            var user = new User { Role = UserRole.Bartender };

            var product = new Product
            {
                Name = "Beer Bottle",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new WasteRegistrationService();

            // Act
            var result = service.RegisterWaste(user, product, amountLost: 2);

            // Assert
            Assert.True(result);
            Assert.Equal(8, product.Stock); // 10 - 2 = 8
        }

        [Fact]
        public void NonBartender_ShouldNotBeAbleTo_RegisterWaste()
        {
            // Arrange
            var user = new User { Role = UserRole.BoardMember }; // Not a bartender

            var product = new Product
            {
                Name = "Beer Bottle",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new WasteRegistrationService();

            // Act
            var result = service.RegisterWaste(user, product, amountLost: 2);

            // Assert
            Assert.False(result);
            Assert.Equal(10, product.Stock); // No change
        }
    }
}
