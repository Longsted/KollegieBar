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
            var user = new User { Role = UserRoles.BoardMember };

            var product = new LiquidWithAlcohol
            {
                Name = "Beer",
                StockQuantity = 50,
                SalesPrice = 20m
            };

            var service = new StockLimitService(initialMaxStock: 100);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 30);

            Assert.True(result);

            Assert.Equal(30, service.MaxStock);

            Assert.Equal(30, product.StockQuantity);
        }

        // Tests that a non-board member cannot update max stock and no changes are applied.

        [Fact]
        public void BoardMember_StockShouldRemain_WhenBelowNewMax()
        {
            var user = new User { Role = UserRoles.Bartender };

            var product = new LiquidWithoutAlcohol()
            {
                Name = "Soda",
                StockQuantity = 10,
                SalesPrice = 10m
            };

            var service = new StockLimitService(initialMaxStock: 20);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 50);

            Assert.True(result);

            Assert.Equal(50, service.MaxStock);

            Assert.Equal(10, product.StockQuantity);
        }

        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_UpdateMaxStock()
        {
            var user = new User { Role = UserRoles.Bartender }; 

            var product = new LiquidWithAlcohol
            {
                Name = "Beer",
                StockQuantity = 40,
                SalesPrice = 20m
            };

            var service = new StockLimitService(initialMaxStock: 100);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 30);

            Assert.False(result);

            Assert.Equal(100, service.MaxStock);

            Assert.Equal(40, product.StockQuantity);
        }
    }
}
