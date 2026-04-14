using Xunit;

namespace UnitTests
{

    public class UserStory9
    {
        // Tests that a bartender can customize a drink before selling it.
        [Fact]
        public void Bartender_ShouldBeAbleTo_CustomizeDrink()
        {
            var user = new User { Role = UserRole.Bartender };

            var product = new Product
            {
                Name = "Special Drink",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(
                user,
                product,
                newPrice: 35m,          
                newType: DrinkType.Spirit
            );

            Assert.True(result);
            Assert.Equal(35m, product.Price);
            Assert.Equal(DrinkType.Spirit, product.Type);
        }


        // Tests that a non-bartender cannot customize a drink and no changes are applied.

        [Fact]
        public void NonBartender_ShouldNotBeAbleTo_CustomizeDrink()
        {
            var user = new User { Role = UserRole.BoardMember }; 

            var product = new Product
            {
                Name = "Special Drink",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(
                user,
                product,
                newPrice: 35m,
                newType: DrinkType.Spirit
            );

            Assert.False(result);
            Assert.Equal(20m, product.Price);
            Assert.Equal(DrinkType.Beer, product.Type);
        }
    }
}
