using Xunit;
using System.Collections.Generic;

namespace UnitTests
{
    public class UserStory2
    {
        [Fact]
        public void GetStatistics_ReturnsData_WhenBoardMemberIsLoggedIn()
        {
            var user = new User { Role = UserRole.BoardMember };

            var salesData = new List<Product>
            {
                new Product { Name = "Beer", Stock = 10 },
                new Product { Name = "Cider", Stock = 5 }
            };

            var service = new SalesStatisticsService(salesData);
            var result = service.GetStatistics(user);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetStatistics_ReturnsEmpty_WhenUserIsNotLoggedIn()
        {
            var user = new User { Role = null };

            var salesData = new List<Product>
            {
                new Product { Name = "Beer", Stock = 10 },
                new Product { Name = "Cider", Stock = 5 }
            };

            var service = new SalesStatisticsService(salesData);
            var result = service.GetStatistics(user);

            Assert.Empty(result);
        }
    }
}
