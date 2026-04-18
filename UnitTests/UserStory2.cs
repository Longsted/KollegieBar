using Xunit;
using System.Collections.Generic;
using DataTransferObject.Model;

namespace UnitTests
{
    public class UserStory2
    {
        // Tests that statistics are given when requested and logged in as Board member.
        [Fact]
        public void GetStatistics_ReturnsData_WhenBoardMemberIsLoggedIn()
        {
            var user = new UserDto { Role = UserRole.BoardMember };

            var salesData = new List<ProductDto>
            {
                new LiquidWithAlcohol { Name = "Beer", StockQuantity = 10 },
                new LiquidWithAlcohol { Name = "Cider", StockQuantity = 5 }
            };

            var service = new SalesStatisticsService(salesData);
            var result = service.GetStatistics(user);

            Assert.NotEmpty(result);
        }

        // Tests that statistics are not given when requested and not logged in as Board member.
        [Fact]
        public void GetStatistics_ReturnsEmpty_WhenUserIsNotLoggedIn()
        {
            var user = new UserDto { Role = UserRole.Bartender };

            var salesData = new List<ProductDto>
            {
                new LiquidWithAlcohol { Name = "Beer", StockQuantity = 10 },
                new LiquidWithAlcohol { Name = "Cider", StockQuantity = 5 }
            };

            var service = new SalesStatisticsService(salesData);
            var result = service.GetStatistics(user);

            Assert.Empty(result);
        }
    }
}
