using System.Collections.Generic;
using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{
    
    public class UserStory7
    {
        // Tests that a board member can delete a product and it is removed from the database.
        [Fact]
        public void BoardMember_ShouldBeAbleTo_DeleteProduct()
        {
            var user = new User { Role = UserRoles.BoardMember };

            var products = new List<Product>
            {
                new LiquidWithAlcohol { Name = "Beer", StockQuantity = 10, CostPrice = 20m },
                new Snack { Name = "Chips", StockQuantity = 5, CostPrice = 15m }
            };

            var productToDelete = products[0];
            var service = new ProductDeletionService();
            
            var result = service.DeleteProduct(user, products, productToDelete);

            Assert.True(result);
            Assert.DoesNotContain(productToDelete, products);
            Assert.Single(products);
        }

        // Tests that a non-board member cannot delete a product and nothing is removed.
        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_DeleteProduct()
        {
            var user = new User { Role = UserRoles.Bartender };

            var products = new List<Product>
            {
                new LiquidWithAlcohol { Name = "Beer", StockQuantity = 10, CostPrice = 20m }
            };

            var productToDelete = products[0];
            var service = new ProductDeletionService();

            var result = service.DeleteProduct(user, products, productToDelete);

            Assert.False(result);
            Assert.Contains(productToDelete, products);
            Assert.Single(products);
        }
    }
}
