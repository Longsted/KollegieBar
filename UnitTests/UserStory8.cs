using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    
    

    public class UserStory8
    {
        // Tests that a board member sees products grouped by category.
        [Fact]
        public void BoardMember_ShouldSee_ProductsGroupedByCategory()
        {
            var user = new User { Role = UserRole.BoardMember };

            var products = new List<Product>
            {
                new Product { Name = "Beer A", Stock = 10, Type = DrinkType.Beer, Price = 20m },
                new Product { Name = "Beer B", Stock = 5, Type = DrinkType.Beer, Price = 18m },
                new Product { Name = "Cider A", Stock = 7, Type = DrinkType.Cider, Price = 15m },
                new Product { Name = "Soda A", Stock = 12, Type = DrinkType.Soda, Price = 10m }
            };

            var service = new ProductCategoryService();

            var result = service.GetProductsByCategory(user, products);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count); 

            Assert.Equal(2, result[DrinkType.Beer].Count);
            Assert.Single(result[DrinkType.Cider]);
            Assert.Single(result[DrinkType.Soda]);
        }

        // Tests that a non-board member receives an empty category list.

        [Fact]
        public void NonBoardMember_ShouldReceive_EmptyCategoryList()
        {
            var user = new User { Role = UserRole.Bartender };

            var products = new List<Product>
            {
                new Product { Name = "Beer A", Stock = 10, Type = DrinkType.Beer, Price = 20m }
            };

            var service = new ProductCategoryService();

            var result = service.GetProductsByCategory(user, products);

            Assert.Empty(result);
        }
    }
}
