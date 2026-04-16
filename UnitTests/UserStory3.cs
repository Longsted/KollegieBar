using BusinessLogic.BusinessLogicLayer;
using Data.Repositories;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory3
    {
        private readonly Mock<ProductRepository> _mockRepository;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory3()
        {
            _mockRepository = new Mock<ProductRepository>(null);
            _service = new ProductBusinessLogicLayer(_mockRepository.Object);
        }

        [Fact]
        public void RegisterIncomingStock_ShouldUpdateStock_WhenProductExists()
        {
            // Create a mock product object
            var existingProduct = new Snack { Id = 1, StockQuantity = 10 };

            // Setup the mock repository to return the product when GetProduct is called
            _mockRepository.Setup(x => x.GetProduct(1)).Returns(existingProduct);

            // Act
            _service.RegisterIncomingStock(1, 30);

            // Assert
            // Verify that UpdateStock was called exactly once with the correct new total
            _mockRepository.Verify(x => x.UpdateStock(1, 40), Times.Once);
        }

        [Fact]
        public void RegisterIncomingStock_ShouldThrowException_WhenProductNotFound()
        {
            // Setup the mock to return null to simulate that the product does not exist in the database
            _mockRepository.Setup(x => x.GetProduct(999)).Returns((Product)null);

            // Act & Assert
            // Verify that an ArgumentException is thrown with the specific error message
            var ex = Assert.Throws<ArgumentException>(() => _service.RegisterIncomingStock(999, 10));
            Assert.Equal("Product not found", ex.Message);

            // Ensure that UpdateStock is never called if the product is missing
            _mockRepository.Verify(x => x.UpdateStock(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void RegisterIncomingStock_ShouldThrowException_WhenQuantityIsNegative()
        {
            // Act & Assert
            // Verify that the validation for negative quantity works correctly
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.RegisterIncomingStock(1, -5));
            Assert.Equal("Invalid quantity", ex.Message);

            // Ensure no database calls are made when the input is invalid
            _mockRepository.Verify(x => x.GetProduct(It.IsAny<int>()), Times.Never);
            _mockRepository.Verify(x => x.UpdateStock(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}