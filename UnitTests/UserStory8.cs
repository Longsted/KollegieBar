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
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.BoardMember };

            var products = new List<ProductDto>
            {
                new LiquidDataTransferObject { Name = "Beer A", StockQuantity = 10, CostPrice = 20m },
                new LiquidDataTransferObject { Name = "Beer B", StockQuantity = 5, CostPrice = 18m },
                new SnackDataTransferObject { Name = "Chips", StockQuantity = 7, CostPrice = 15m },
                new LiquidDataTransferObject { Name = "Soda", StockQuantity = 12, CostPrice = 10m }
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
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.Bartender };

            var products = new List<ProductDto>
            {
                new LiquidDataTransferObject { Name = "Beer A", StockQuantity = 10, CostPrice = 20m }
            };

            var service = new ProductCategoryService();

            var result = service.GetProductsByCategory(user, products);

            Assert.Empty(result);
        }
    }
}
