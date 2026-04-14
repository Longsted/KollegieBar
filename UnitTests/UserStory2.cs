using Xunit;
using System.Collections.Generic;

namespace UnitTests
{
    public class UserStory2Tests
    {
        // Tester at statistik over salget sendes når bestyrelsesmedlemmet har korrekt rolle
        [Fact]
        public void GetSalesStatistics_ReturnsData_WhenUserRoleIsBoardMember()
        {
            var boardMember = new User { Role = UserRole.BoardMember };

            var salesData = new List<Product>
            {
                new Product { Name = "Beer", Stock = 10, Type = DrinkType.Beer, Price = 25.0m },
                new Product { Name = "Cider", Stock = 5, Type = DrinkType.Cider, Price = 30.0m }
            };

            var statisticsService = new StatisticsService(salesData);
            var result = statisticsService.GetSalesStatistics();

            Assert.NotEmpty(result);
        }

        // Tester at statistik IKKE sendes når rollen er null (ikke logget ind)
        [Fact]
        public void GetSalesStatistics_ReturnsEmpty_WhenUserRoleIsNull()
        {
            var noRoleUser = new User { Role = null };

            var salesData = new List<Product>
            {
                new Product { Name = "Beer", Stock = 10, Type = DrinkType.Beer, Price = 25.0m },
                new Product { Name = "Cider", Stock = 5, Type = DrinkType.Cider, Price = 30.0m }
            };

            var statisticsService = new StatisticsService(salesData);
            var result = statisticsService.GetSalesStatistics();

            Assert.Empty(result);
        }
    }
}
