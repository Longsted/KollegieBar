using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{

    public class UserStory9
    {
        // Tests that a bartender can customize a drink before selling it.
        [Fact]
        public void Bartender_ShouldBeAbleTo_CustomizeDrink()
        {
            var user = new User { Role = UserRoles.Bartender };

            var product = new Drink()
            {
                Name = "Special Drink",
                SalesPrice = 20
            };

            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(
                user,
                product,
                newPrice: 35
            );

            Assert.True(result);
            Assert.Equal(35, product.CostPrice);
            // Assert.Equal(DrinkType.Spirit, product.Type);
        }

        // Tests that a non-bartender cannot customize a drink and no changes are applied.

        [Fact]
        public void NonBartender_ShouldNotBeAbleTo_CustomizeDrink()
        {
            var user = new User { Role = UserRoles.BoardMember }; 

            var product = new Drink
            {
                Name = "Special Drink",
                SalesPrice = 20
            };

            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(
                user,
                product,
                newPrice: 35
            );

            Assert.False(result);
            Assert.Equal(20, product.SalesPrice);
        }
    }
}
