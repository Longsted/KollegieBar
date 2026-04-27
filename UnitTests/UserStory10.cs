using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{
  
    public class UserStory10
    {
        // Tests that a bartender can register waste and the product stock decreases.
        [Fact]
        public void Bartender_ShouldBeAbleTo_RegisterWaste()
        {
            var user = new UserDataTransferObject
            {
                RoleDataTransferObject = UserRoleDataTransferObject.Bartender
            };

            var product = new LiquidDataTransferObject
            {
                Name = "Beer Bottle",
                StockQuantity = 10
            };

            var service = new WasteRegistrationService();

            var result = service.RegisterWaste(
                user,
                product,
                amountLost: 2
                );

            Assert.True(result);
            Assert.Equal(8, product.StockQuantity); 
        }

        // Tests that a non-bartender cannot register waste and stock remains unchanged.

        [Fact]
        public void NonBartender_ShouldNotBeAbleTo_RegisterWaste()
        {
            var user = new UserDataTransferObject
            {
                RoleDataTransferObject = UserRoleDataTransferObject.BoardMember
            };

            var product = new LiquidDataTransferObject
            {
                Name = "Beer Bottle",
                StockQuantity = 10
            };

            var service = new WasteRegistrationService();

            var result = service.RegisterWaste(
                user,
                product,
                amountLost: 2
                );

            Assert.False(result);
            Assert.Equal(10, product.StockQuantity); 
            
        }
        
        
        [Fact]
        public void RegisterWaste_InvalidAmount_ShouldReturnFalse()
        {
            var user = new UserDataTransferObject
            {
                RoleDataTransferObject = UserRoleDataTransferObject.Bartender
            };

            var product = new LiquidDataTransferObject
            {
                Name = "Beer Bottle",
                StockQuantity = 10
            };
            
            var service = new WasteRegistrationService();

            var result = service.RegisterWaste(
                user, product, amountLost: 0);
            
            Assert.False(result);
            Assert.Equal(10, product.StockQuantity);
            
        }

        [Fact]
        public void RegisterWaste_ShouldNotAllowNegativeStock()
        {
            var user = new UserDataTransferObject
            {
                RoleDataTransferObject = UserRoleDataTransferObject.Bartender
            };

            var product = new LiquidDataTransferObject
            {
                Name = "Beer Bottle",
                StockQuantity = 1
            };
            
            var service = new WasteRegistrationService();

            var result = service.RegisterWaste(
                user, product, amountLost: 5);
            
            Assert.True(result);
            Assert.Equal(0, product.StockQuantity);

        }
        
    }
    
}
