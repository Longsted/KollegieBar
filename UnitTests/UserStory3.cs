using Xunit;

namespace UnitTests
{
    public class UserStory3
    {
        [Fact]
        public void RegisterIncomingStock_UpdatesStock_WhenBoardMemberIsLoggedIn()
        {
            var user = new User { Role = UserRole.BoardMember };
            var product = new Product { Name = "Beer", Stock = 10 };

            var service = new IncomingStockService();
            var result = service.RegisterIncomingStock(user, product, 30);

            Assert.True(result);
            Assert.Equal(30, product.Stock);
        }

        [Fact]
        public void RegisterIncomingStock_DoesNotUpdateStock_WhenUserIsNotLoggedIn()
        {
            var user = new User { Role = null };
            var product = new Product { Name = "Beer", Stock = 10 };

            var service = new IncomingStockService();
            var result = service.RegisterIncomingStock(user, product, 30);

            Assert.False(result);
            Assert.Equal(10, product.Stock);
        }
    }
}

