using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory4
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory4()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockProductRepo = new Mock<IProductRepository>();

            _mockUow.Setup(u => u.Products).Returns(_mockProductRepo.Object);

            _service = new ProductBusinessLogicLayer(_mockUow.Object);
        }

        [Fact]
        public async Task CreateProduct_ShouldCallRepository_WhenProductIsValid()
        {
            var dto = new DataTransferObject.Model.LiquidWithAlcohol
            {
                Name = "Thy Classic",
                CostPrice = 10m,
                StockQuantity = 100,
                VolumeCl = 33,
                SalesPrice = 25m,
                AlcoholPercentage = 4.6
            };

            await _service.CreateProductAsync(dto);

            _mockProductRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_ShouldThrowException_WhenNameIsEmpty()
        {
            var dto = new DataTransferObject.Model.Snack
            {
                Name = "",
                CostPrice = 5m,
                StockQuantity = 50,
                SalesPrice = 15m
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.CreateProductAsync(dto));

            Assert.Equal("Name is required", exception.Message);

            _mockProductRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateProduct_ShouldThrowException_WhenPriceIsNegative()
        {
            var dto = new DataTransferObject.Model.LiquidWithAlcohol
            {
                Name = "Free Beer",
                CostPrice = -1m,
                StockQuantity = 100,
                VolumeCl = 33,
                SalesPrice = 0m,
                AlcoholPercentage = 4.6
            };

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.CreateProductAsync(dto));
        }

        [Fact]
        public async Task CreateLiquidWithAlcohol_ShouldThrowException_WhenAlcoholPercentageIsInvalid()
        {
            var dto = new DataTransferObject.Model.LiquidWithAlcohol
            {
                Name = "Magic",
                CostPrice = 10m,
                StockQuantity = 10,
                VolumeCl = 33,
                SalesPrice = 50m,
                AlcoholPercentage = 110
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.CreateProductAsync(dto));

            Assert.Equal("Invalid alcohol percentage", exception.Message);
        }

        [Fact]
        public async Task CreateSnack_ShouldWorkWithValidData()
        {
            var dto = new DataTransferObject.Model.Snack
            {
                Name = "Chips",
                CostPrice = 5m,
                StockQuantity = 50,
                SalesPrice = 15m
            };

            await _service.CreateProductAsync(dto);

            _mockProductRepo.Verify(r =>
                r.AddAsync(It.Is<Product>(p => p.Name == "Chips")), Times.Once);
        }
    }
}