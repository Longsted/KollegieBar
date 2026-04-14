using Xunit;

namespace UnitTests
{
    public class UserStory3
    {
        // Tests that incoming stock is registered and stock is updated when logged in as Board member.
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

        // Tests that incoming stock is not registered and stock is not updated when not logged in as Board member.
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

