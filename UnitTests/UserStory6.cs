using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{
    public class UserStory6
    {
        // Tests that a board member can edit an existing product and the system updates it.
        [Fact]
        public void BoardMember_ShouldBeAbleTo_EditProductInformation()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.BoardMember };
            var product = new LiquidDataTransferObject
            {
                Name = "Old Beer",
                StockQuantity = 10,
                CostPrice = 20m
            };

            var service = new ProductEditingService();

            var result = service.EditProduct(
                user,
                product,
                newName: "New Beer",
                newStock: 25,
                newPrice: 30m
            );

            Assert.True(result);
            Assert.Equal("New Beer", product.Name);
            Assert.Equal(25, product.StockQuantity);
            Assert.Equal(30m, product.CostPrice);
        }

        // Tests that a non-board member cannot edit a product and no changes are applied.

        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_EditProductInformation()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.Bartender }; // Not a board member
            var product = new LiquidDataTransferObject
            {
                Name = "Beer",
                StockQuantity = 10,
                CostPrice = 20m
            };

            var service = new ProductEditingService();

            var result = service.EditProduct(
                user,
                product,
                newName: "New Beer",
                newStock: 25,
                newPrice: 30m
            );

            Assert.False(result);
            Assert.Equal("Beer", product.Name);
            Assert.Equal(10, product.StockQuantity);
            Assert.Equal(20m, product.CostPrice);
        }
    }
}
