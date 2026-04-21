using System.Collections.Generic;
using System.Linq;
using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{
    public class UserStory5
    {
        // Tests that a board member receives a list of low inventory items when logged in.
        [Fact]
        public void BoardMember_ShouldReceive_ListOfLowInventoryItems()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.BoardMember };

            var products = new List<ProductDataTransferObject>
            {
                new LiquidDataTransferObject { Name = "Beer", StockQuantity = 2, CostPrice = 10 },
                new LiquidDataTransferObject { Name = "Cider", StockQuantity = 10, CostPrice = 12 },
                new LiquidDataTransferObject { Name = "Soda", StockQuantity = 1, CostPrice = 8 },
                new SnackDataTransferObject { Name = "Chips", StockQuantity = 20, CostPrice = 50 }
            };

            const int threshold = 5;
            var service = new LowInventoryService(products, threshold);

            var lowInventory = service.GetLowInventory(user);

            Assert.NotNull(lowInventory);
            Assert.Equal(2, lowInventory.Count);

            Assert.Contains(lowInventory, p => p.Name == "Beer");
            Assert.Contains(lowInventory, p => p.Name == "Soda");

            Assert.DoesNotContain(lowInventory, p => p.Name == "Cider");
            Assert.DoesNotContain(lowInventory, p => p.Name == "Chips");
        }

        // Tests that a non-board member receives an empty list when requesting low inventory items.
        [Fact]
        public void NonBoardMember_ShouldReceive_EmptyList()
        {
            var user = new UserDataTransferObject { RoleDataTransferObject = UserRoleDataTransferObject.Bartender };

            var products = new List<ProductDataTransferObject>
                {
                new LiquidDataTransferObject { Name = "Beer", StockQuantity = 1, CostPrice = 10 }
                };

            var service = new LowInventoryService(products, 5);

            var result = service.GetLowInventory(user);

            Assert.Empty(result); 
        }
    }
}
