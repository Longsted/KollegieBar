using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{
    public class UserStory1
    {
        // Tests that stock is reduced when logged in and sale is registered. 
        [Fact]
        public void RegisterSale_ReducesStock_WhenBartenderIsLoggedIn()
        {
            var user = new User { Role = UserRoles.Bartender };
            var product = new LiquidWithAlcohol { Name = "Beer", StockQuantity = 10  };

            var service = new SaleService();
            var result = service.RegisterSale(user, product);

            Assert.True(result);
            Assert.Equal(9, product.StockQuantity);
        }

        // Tests that Stock should not reduce user is boardmember and sale is registered.
        [Fact]
        public void RegisterSale_DoesNotReduceStock_WhenUserJustDoSomething()
        {
            var user = new User { Role = UserRoles.BoardMember };
            var product = new LiquidWithAlcohol { Name = "Beer", StockQuantity = 10 };

            var service = new SaleService();
            var result = service.RegisterSale(user, product);

            Assert.False(result);
            Assert.Equal(10, product.StockQuantity);
        }
    }
}
