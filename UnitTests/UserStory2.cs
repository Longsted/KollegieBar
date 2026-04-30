using Xunit;
using System.Collections.Generic;
using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
using Xunit.Abstractions;

namespace UnitTests
{
    public class UserStory2
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<IStatisticsLogicLayer> _mockStatisticsRepository;
        private readonly IStatisticsLogicLayer _statisticsLogicLayer;

        public UserStory2(ITestOutputHelper testOutputHelper)
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockSalesRepository = new Mock<ISalesRepository>();

            // Setup Unit of Work to return of all of the repositories
            _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(u => u.Sales).Returns(_mockSalesRepository.Object);

            _statisticsLogicLayer = new StatisticsLogicLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task DashBoardStastisticsAsync_ReturnsCorrectData()
        {
            var beer = new Liquid("Beer", 15, 100, 33, 4.6) { Id = 1 };
            var soda = new Liquid("Soda", 10, 100, 33, 0.0) { Id = 2 };

            var transactionId = Guid.NewGuid();

            var sales = new List<Sale>
            {
                new Sale(DateTime.Now, transactionId, beer),
                new Sale(DateTime.Now, transactionId, soda),
                new Sale(DateTime.Now, transactionId, soda)
            };

            var products = new List<Product> { beer, soda };
            beer.MinStockQuantity = 5;
            soda.MinStockQuantity = 5;

            _mockSalesRepository.Setup(r => r.GetSalesByDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(sales);
            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var result = await _statisticsLogicLayer.DashBoardStastisticsAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.TotalSalesThisFriday);
            Assert.Equal(0, result.LowStockCount);
            Assert.Equal("Soda", result.MostSoldItem.Name);
            Assert.Equal(2, result.MostSoldItem.Count);
        }

        [Fact]
        public async Task DashBoardStastisticsAsync_ShouldHandleZeroSales()
        {
            _mockSalesRepository.Setup(r => r.GetSalesByDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<Sale>());

            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product>());

            var result = await _statisticsLogicLayer.DashBoardStastisticsAsync();

            Assert.Equal(0, result.TotalSalesThisFriday);
            Assert.Null(result.MostSoldItem);
        }

        [Fact]
        public async Task GetWinner_ShouldReturnFirstGroup_WhenCountsAreEqual()
        {
            var beer = new Liquid("Beer", 15, 100, 33, 4.6) { Id = 1 };
            var drink = new Drink { Id = 10, Name = "Gin Hass" };

            var sales = new List<Sale>
            {
                new Sale(DateTime.Now, Guid.NewGuid(), beer),
                new Sale(DateTime.Now, Guid.NewGuid(), drink)
            };

            _mockSalesRepository.Setup(r => r.GetSalesByDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(sales);

            var result = await _statisticsLogicLayer.SimplePeriodStatisticsAsync(DateTime.Now, DateTime.Now);

            Assert.Equal("Beer", result.MostSoldItem.Name);
        }

        [Fact]
        public async Task AllSalesStatistics_ShouldReturnUnknown_WhenNamesAreMissing()
        {
            var sales = new List<Sale> { new Sale { SaleDate = DateTime.Now } };

            _mockSalesRepository.Setup(r => r.GetSalesByDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(sales);

            var result = await _statisticsLogicLayer.AllSalesStatisticsInPeriodAsync(DateTime.Now, DateTime.Now);

            Assert.Equal("Unknown", result.Items[0].Name);
        }

        [Fact]
        public async Task SimpleperiodStatistics_ShouldPassCorrectDatesToRepository()
        {
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 1, 2);

            _mockSalesRepository.Setup(r => r.GetSalesByDateRange(start, end)).ReturnsAsync(new List<Sale>());

            await _statisticsLogicLayer.SimplePeriodStatisticsAsync(start, end);

            _mockSalesRepository.Verify(r => r.GetSalesByDateRange(start, end), Times.Once);
        }
    }
}