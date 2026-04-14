using Xunit;

namespace UnitTests
{
    public class UserStory1
    {
        // Tests that stock is reduced when logged in and sale is registered. 
        [Fact]
        public void RegisterSale_ReducesStock_WhenBartenderIsLoggedIn()
        {
            var user = new User { Role = UserRole.Bartender };
            var product = new Product { Name = "Beer", Stock = 10 };

            var service = new SaleService();
            var result = service.RegisterSale(user, product);

            Assert.True(result);
            Assert.Equal(9, product.Stock);
        }

        // Tests that Stock should not reduce when not logged in and sale is registered.
        [Fact]
        public void RegisterSale_DoesNotReduceStock_WhenUserIsNotLoggedIn()
        {
            var user = new User { Role = null };
            var product = new Product { Name = "Beer", Stock = 10 };

            var service = new SaleService();
            var result = service.RegisterSale(user, product);

            Assert.False(result);
            Assert.Equal(10, product.Stock);
        }
    }
}
