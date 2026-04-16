using BusinessLogic.BusinessLogicLayer;
using Data.Repositories;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory1
    {
        private readonly Mock<ProductRepository> _mockRepository;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory1()
        {
            _mockRepository = new Mock<ProductRepository>(null);
            _service = new ProductBusinessLogicLayer(_mockRepository.Object);
        }
        
        // Tests that stock is reduced and sale is registered. 
        [Fact]
        public void RegisterSale_ReducesStock()
        {
            var product = new LiquidWithAlcohol {Id = 1, Name = "Beer", StockQuantity = 10  };
            _mockRepository.Setup(x => x.GetProduct(1)).Returns(product);
            
            _service.RegisterSale(1, 2);

            // Assert
            _mockRepository.Verify(x => x.UpdateStock(product.Id, 8), Times.Once);
        }
        
        [Fact]
        public void RegisterSale_ThrowsException_WhenStockIsTooLow()
        {
            // Arrange
            var product = new LiquidWithAlcohol { Id = 1, StockQuantity = 1 }; // Only 1 left
            _mockRepository.Setup(x => x.GetProduct(1)).Returns(product);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _service.RegisterSale(1, 5)); // Prøv at sælge 5
            
            _mockRepository.Verify(x => x.UpdateStock(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
        
        [Fact]
        public void RegisterSale_ThrowsArgumentException_WhenQuantityIsZeroOrLess()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _service.RegisterSale(1, 0));
            Assert.Equal("Invalid quantity", exception.Message);
            
            _mockRepository.Verify(x => x.GetProduct(It.IsAny<int>()), Times.Never);
        }
        
        [Fact]
        public void RegisterSale_ThrowsException_WhenProductDoesNotExist()
        {
            // Arrange: Setup repository til at returnere null
            _mockRepository.Setup(x => x.GetProduct(999)).Returns((LiquidWithAlcohol)null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _service.RegisterSale(999, 1));
        }
    }
}
