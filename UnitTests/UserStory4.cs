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
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory4()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _service = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateProduct_ShouldCallRepository_WhenProductIsValid()
        {
            var liquidWithAlcohol = new DataTransferObject.Model.LiquidDataTransferObject
            {
                Name = "Thy Classic",
                CostPrice = 10m,
                StockQuantity = 100,
                VolumeCl = 33,
                AlcoholPercentage = 4.6
            };

            await _service.CreateProductAsync(liquidWithAlcohol);

            _mockProductRepository.Verify(productRepository => productRepository.AddAsync(It.IsAny<Product>()), Times.Once);
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_ShouldThrowException_WhenNameIsEmpty()
        {
            var snack = new DataTransferObject.Model.SnackDataTransferObject
            {
                Name = "",
                CostPrice = 5m,
                StockQuantity = 50
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProductAsync(snack));

            Assert.Equal("Name is required", exception.Message);

            _mockProductRepository.Verify(repository => repository.AddAsync(It.IsAny<Product>()), Times.Never);
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateProduct_ShouldThrowException_WhenPriceIsNegative()
        {
            var liquidWithAlcohol = new DataTransferObject.Model.LiquidDataTransferObject
            {
                Name = "Free Beer",
                CostPrice = -1m,
                StockQuantity = 100,
                VolumeCl = 33,
                AlcoholPercentage = 4.6
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProductAsync(liquidWithAlcohol));
        }

        [Fact]
        public async Task CreateLiquidWithAlcohol_ShouldThrowException_WhenAlcoholPercentageIsInvalid()
        {
            var liquidWithAlcohol = new DataTransferObject.Model.LiquidDataTransferObject
            {
                Name = "Magic",
                CostPrice = 10m,
                StockQuantity = 10,
                VolumeCl = 33,
                AlcoholPercentage = 110
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProductAsync(liquidWithAlcohol));

            Assert.Equal("Invalid alcohol percentage", exception.Message);
        }

        [Fact]
        public async Task CreateSnack_ShouldWorkWithValidData()
        {
            var snack = new DataTransferObject.Model.SnackDataTransferObject
            {
                Name = "Chips",
                CostPrice = 5m,
                StockQuantity = 50
            };

            await _service.CreateProductAsync(snack);

            _mockProductRepository.Verify(repository => repository.AddAsync(It.Is<Product>(p => p.Name == "Chips")), Times.Once);
        }
    }
}