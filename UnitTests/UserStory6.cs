using Xunit;

namespace UnitTests
{
    public class UserStory6
    {

        // Tests that a board member can edit an existing product and the system updates it.

        [Fact]
        public void BoardMember_ShouldBeAbleTo_EditProductInformation()
        {
            var user = new User { Role = UserRole.BoardMember };
            var product = new Product
            {
                Name = "Old Beer",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new ProductEditingService();

            var result = service.EditProduct(
                user,
                product,
                newName: "New Beer",
                newStock: 25,
                newType: DrinkType.Cider,
                newPrice: 30m
            );

            Assert.True(result);
            Assert.Equal("New Beer", product.Name);
            Assert.Equal(25, product.Stock);
            Assert.Equal(DrinkType.Cider, product.Type);
            Assert.Equal(30m, product.Price);
        }

        // Tests that a non-board member cannot edit a product and no changes are applied.

        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_EditProductInformation()
        {
            var user = new User { Role = UserRole.Bartender }; // Not a board member
            var product = new Product
            {
                Name = "Beer",
                Stock = 10,
                Type = DrinkType.Beer,
                Price = 20m
            };

            var service = new ProductEditingService();

            var result = service.EditProduct(
                user,
                product,
                newName: "New Beer",
                newStock: 25,
                newType: DrinkType.Cider,
                newPrice: 30m
            );

            Assert.False(result);
            Assert.Equal("Beer", product.Name);
            Assert.Equal(10, product.Stock);
            Assert.Equal(DrinkType.Beer, product.Type);
            Assert.Equal(20m, product.Price);
        }
    }
}
