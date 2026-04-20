using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{

    public class UserStory11
    {
        // Tests that a board member can update the global max stock and product stock adjusts if needed.
        [Fact]
        public void BoardMember_ShouldBeAbleTo_UpdateMaxStock_AndAdjustProductStock()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.BoardMember };

            var product = new LiquidDataTransferObject
            {
                Name = "Beer",
                StockQuantity = 50
            };

            var service = new StockLimitService(initialLimit: 100);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 30);

            Assert.True(result);
            Assert.Equal(30, service.MaxStockQuantity);
            Assert.Equal(30, product.MaxStockQuantity);
        }

        // Tests that a non-board member cannot update max stock and no changes are applied.
        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_UpdateMaxStock()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.Bartender }; 

            var product = new LiquidDataTransferObject
            {
                Name = "Beer",
                StockQuantity = 40
            };

            var service = new StockLimitService(initialLimit: 100);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 30);

            Assert.False(result);
            Assert.Equal(100, service.MaxStockQuantity);
            Assert.Equal(40, product.MaxStockQuantity);
        }

        // Tests that a board member can update the global min stock and product stock adjusts if needed.
        [Fact]
        public void BoardMember_ShouldBeAbleTo_UpdateMinStock()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.Bartender };

            var product = new LiquidDataTransferObject
            {
                Name = "Beer",
                StockQuantity = 40
            };

            var service = new StockLimitService(initialLimit: 100);

            var result = service.UpdateMinStock(user, product, newMinStock: 30);

            Assert.False(result);
            Assert.Equal(100, service.MinStockQuantity);
            Assert.Equal(40, product.MinStockQuantity);
        }

        // Tests that a non-board member cannot update min stock and no changes are applied.
        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_UpdateMinStock()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.Bartender };

            var product = new LiquidDataTransferObject
            {
                Name = "Beer",
                StockQuantity = 40
            };

            var service = new StockLimitService(initialLimit: 100);

            var result = service.UpdateMinStock(user, product, newMinStock: 30);

            Assert.False(result);
            Assert.Equal(100, service.MinStockQuantity);
            Assert.Equal(40, product.MinStockQuantity);
        }
    }
}
