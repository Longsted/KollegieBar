using System.Collections.Generic;
using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{
    
    public class UserStory8
    {
        // Tests that a board member sees products grouped by category.
        [Fact]
        public void BoardMember_ShouldSee_ProductsGroupedByCategory()
        {
            var user = new UserDto { Role = UserRole.BoardMember };

            var products = new List<ProductDto>
            {
                new LiquidWithAlcohol { Name = "Beer A", StockQuantity = 10, CostPrice = 20m },
                new LiquidWithAlcohol { Name = "Beer B", StockQuantity = 5, CostPrice = 18m },
                new Snack { Name = "Chips", StockQuantity = 7, CostPrice = 15m },
                new LiquidWithoutAlcohol { Name = "Soda", StockQuantity = 12, CostPrice = 10m }
            };

            var service = new ProductCategoryService();

            var result = service.GetProductsByCategory(user, products);

            Assert.NotNull(result);
            
            Assert.Equal(3, result.Count); 
            
            Assert.Equal(2, result["LiquidWithAlcohol"].Count);
            Assert.Single(result["Snack"]);
            Assert.Single(result["LiquidWithoutAlcohol"]);
        }

        // Tests that a non-board member receives an empty category list.

        [Fact]
        public void NonBoardMember_ShouldReceive_EmptyCategoryList()
        {
            var user = new UserDto { Role = UserRole.Bartender };

            var products = new List<ProductDto>
            {
                new LiquidWithAlcohol { Name = "Beer A", StockQuantity = 10, CostPrice = 20m }
            };

            var service = new ProductCategoryService();

            var result = service.GetProductsByCategory(user, products);

            Assert.Empty(result);
        }
    }
}
