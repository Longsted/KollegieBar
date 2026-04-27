using Data.Interfaces;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{

    public class UserStory9
    {
        // Tests that a bartender can customize a drink before selling it.
        [Fact]
        public void Bartender_ShouldBeAbleTo_CustomizeDrink()
        {
            var user = new UserDataTransferObject
            {
                RoleDataTransferObject = UserRoleDataTransferObject.Bartender
            };
            
            var drink = new DrinkDataTransferObject
            {
                Name = "Special Drink",
                CostPrice = 25
            };
            
            var service = new DrinkCustomizationService();
            
            var result = service.CustomizeDrink(
                user,
                drink,
                newPrice: 35
            );

            Assert.True(result);
            Assert.Equal(35, drink.CostPrice);
        }

        // Tests that a non-bartender cannot customize a drink and no changes are applied.
        [Fact]
        public void NonBartender_ShouldNotBeAbleTo_CustomizeDrink()
        {
            var user = new UserDataTransferObject
            {
                RoleDataTransferObject = UserRoleDataTransferObject.BoardMember
            }; 

            var drink = new DrinkDataTransferObject
            {
                Name = "Special Drink",
                CostPrice = 25
            };

            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(
                user,
                drink,
                newPrice: 35
            );

            Assert.False(result);
            Assert.Equal(25, drink.CostPrice);
            
        }

        [Fact]
        public void Bartender_ShouldNotBeAbleTo_SetNegativePrice()
        {
            var user = new UserDataTransferObject
            {
                RoleDataTransferObject = UserRoleDataTransferObject.Bartender
            };

            var drink = new DrinkDataTransferObject
            {
                Name = "Special Drink",
                CostPrice = 25
            };
            
            var service = new DrinkCustomizationService();

            var result = service.CustomizeDrink(user, drink, newPrice: -10);
            
            Assert.False(result);
            Assert.Equal(25, drink.CostPrice);
        }
    }
}
