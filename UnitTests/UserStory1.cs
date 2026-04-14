using Xunit;

namespace UnitTests
{
    public class UserStory1
    {
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
