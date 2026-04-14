using Xunit;

namespace UnitTests
{

    public class UserStory11
    {
        // Tests that a board member can update the global max stock and product stock adjusts if needed.

        [Fact]
        public void BoardMember_ShouldBeAbleTo_UpdateMaxStock_AndAdjustProductStock()
        {
            var user = new User { Role = UserRole.BoardMember };

            var product = new Product
            {
                Name = "Beer",
                Stock = 50,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new StockLimitService(initialMaxStock: 100);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 30);

            Assert.True(result);

            Assert.Equal(30, service.MaxStock);

            Assert.Equal(30, product.Stock);
        }

        // Tests that a non-board member cannot update max stock and no changes are applied.

        [Fact]
        public void BoardMember_StockShouldRemain_WhenBelowNewMax()
        {
            var user = new User { Role = UserRole.BoardMember };

            var product = new Product
            {
                Name = "Cider",
                Stock = 10,
                Type = DrinkType.Cider,
                Price = 15m
            };

            var service = new StockLimitService(initialMaxStock: 20);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 50);

            Assert.True(result);

            Assert.Equal(50, service.MaxStock);

            Assert.Equal(10, product.Stock);
        }

        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_UpdateMaxStock()
        {
            var user = new User { Role = UserRole.Bartender }; 

            var product = new Product
            {
                Name = "Beer",
                Stock = 40,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new StockLimitService(initialMaxStock: 100);

            var result = service.UpdateMaxStock(user, product, newMaxStock: 30);

            Assert.False(result);

            Assert.Equal(100, service.MaxStock);

            Assert.Equal(40, product.Stock);
        }
    }
}
