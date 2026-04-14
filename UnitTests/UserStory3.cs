using Xunit;

namespace UnitTests
{
    public class UserStory3
    {
        [Fact]
        public void UpdateStock_UpdatesStock_WhenBoardMemberIsLoggedIn()
        {
            var user = new User { Role = UserRole.BoardMember };
            var product = new Product { Name = "Beer", Stock = 10 };

            var service = new InventoryUpdateService();
            var result = service.UpdateStock(user, product, 25);

            Assert.True(result);
            Assert.Equal(25, product.Stock);
        }

        [Fact]
        public void UpdateStock_DoesNotUpdateStock_WhenUserIsNotLoggedIn()
        {
            var user = new User { Role = null };
            var product = new Product { Name = "Beer", Stock = 10 };

            var service = new InventoryUpdateService();
            var result = service.UpdateStock(user, product, 25);

            Assert.False(result);
            Assert.Equal(10, product.Stock);
        }
    }
}
