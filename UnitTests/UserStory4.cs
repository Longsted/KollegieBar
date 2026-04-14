using Xunit;

namespace UnitTests
{
    public class UserStory4
    {
        // Tests that a new product is created when a board member is logged in.
        [Fact]
        public void CreateProduct_ReturnsNewProduct_WhenBoardMemberIsLoggedIn()
        {
            var user = new User { Role = UserRole.BoardMember };

            var service = new ProductCreationService();
            var product = service.CreateProduct(user, "NewBeer", 50, DrinkType.Beer, 25m);

            Assert.NotNull(product);
            Assert.Equal("NewBeer", product.Name);
            Assert.Equal(50, product.Stock);
        }

        // Tests that a new product is not created when a user is not logged in.
        [Fact]
        public void CreateProduct_ReturnsNull_WhenUserIsNotLoggedIn()
        {
            var user = new User { Role = null };

            var service = new ProductCreationService();
            var product = service.CreateProduct(user, "NewBeer", 50, DrinkType.Beer, 25m);

            Assert.Null(product);
        }
    }
}

