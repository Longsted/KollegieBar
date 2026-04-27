using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using Moq;
using Xunit;

namespace UnitTests
{

    public class UserStory11
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory11()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _service = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
        }

        // Tests that GetProductAsync returns the correct product details when a valid ID is provided.
        [Fact]
        public async Task GetProductAsync_ShouldReturn_Product()
        {
            var product = new Liquid
            {
                Id = 1,
                Name = "Beer",
                StockQuantity = 50,
                MaxStockQuantity = 100,
                MinStockQuantity = 10
            };

            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product);

            var result = await _service.GetProductAsync(1);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.StockQuantity, result.StockQuantity);
            Assert.Equal(product.MaxStockQuantity, result.MaxStockQuantity);
            Assert.Equal(product.MinStockQuantity, result.MinStockQuantity);
        }

        // Tests that UpdateMaxStockAsync successfully updates the max stock quantity for a valid product ID.
        [Fact]
        public async Task ShouldBeAbleTo_UpdateMaxStock()
        {
            var newMaxStock = 30;

            var product = new Liquid
            {
                Id = 1,
                Name = "Beer",
                StockQuantity = 50,
                MaxStockQuantity = 100,
                MinStockQuantity = 10
            };

            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product);
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.SaveChangesAsync()).Returns(Task.CompletedTask);

            await _service.UpdateMaxStockAsync(product.Id, newMaxStock);

            Assert.Equal(newMaxStock, product.MaxStockQuantity);

            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Once);
        }

        //Tests that UpdateMinStockAsync successfully updates the min stock quantity for a valid product ID.
        [Fact]
        public async Task ShouldBeAbleTo_UpdateMinStock()
        {
            var newMinStock = 30;

            var product = new Liquid
            {
                Id = 1,
                Name = "Beer",
                StockQuantity = 50,
                MaxStockQuantity = 100,
                MinStockQuantity = 10
            };

            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product);
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.SaveChangesAsync()).Returns(Task.CompletedTask);

            await _service.UpdateMinStock(product.Id, newMinStock);

            Assert.Equal(newMinStock, product.MinStockQuantity);

            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Once);
        }

        //Tests that UpdateMinStock throws an exception when trying to update min stock to a negative value.
        [Fact]
        public async Task UpdateMinStock_ShouldThrowArgumentException_WhenStockIsNegative()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.UpdateMinStock(1, -10));

            Assert.Equal("Invalid stock", exception.Message);
        }

        //Tests that UpdateMaxStock throws an exception when trying to update max stock to a negative value.
        [Fact]
        public async Task UpdateMaxStock_ShouldThrowArgumentException_WhenStockIsNegative()
        {

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.UpdateMaxStockAsync(1, -10));

            Assert.Equal("Invalid stock", exception.Message);
        }

        // Tests that UpdateMaxStock throws an exception when trying to update max stock for a non-existent product.
        [Fact]
        public async Task UpdateMaxStock_ShouldThrowInvalidOperationException_WhenProductDoesNotExist()
        {
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(999)).ReturnsAsync((Product)null);
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.UpdateMaxStockAsync(999, 30));
        }

        // Tests that UpdateMinStock throws an exception when trying to update min stock for a non-existent product.
        [Fact]
        public async Task UpdateMinStock_ShouldThrowInvalidOperationException_WhenProductDoesNotExist()
        {
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(999)).ReturnsAsync((Product)null);
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.UpdateMinStock(999, 30));
        }
    }
}
