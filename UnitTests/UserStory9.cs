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
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.Bartender };

            var product = new DrinkDataTransferObject()
            {
                Name = "Special Drink"
            };

            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(
                user,
                product,
                newPrice: 35
            );

            Assert.True(result);
            Assert.Equal(35, product.CostPrice);
        }

        // Tests that a non-bartender cannot customize a drink and no changes are applied.
        [Fact]
        public void NonBartender_ShouldNotBeAbleTo_CustomizeDrink()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.BoardMember }; 

            var product = new DrinkDataTransferObject
            {
                Name = "Special Drink"
            };

            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(
                user,
                product,
                newPrice: 35
            );

            Assert.False(result);
        }
    }
}
