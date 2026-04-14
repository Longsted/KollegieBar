using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    

    public class UserStory7
    {
        // Tests that a board member can delete a product and it is removed from the database.
        [Fact]
        public void BoardMember_ShouldBeAbleTo_DeleteProduct()
        {
            var user = new User { Role = UserRole.BoardMember };

            var products = new List<Product>
            {
                new Product { Name = "Beer", Stock = 10, Type = DrinkType.Beer, Price = 20m },
                new Product { Name = "Cider", Stock = 5, Type = DrinkType.Cider, Price = 15m }
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
            var user = new User { Role = UserRole.Bartender };

            var products = new List<Product>
            {
                new Product { Name = "Beer", Stock = 10, Type = DrinkType.Beer, Price = 20m }
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
