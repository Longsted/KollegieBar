using BusinessLogic.BusinessLogicLayer;
using Data.Repositories;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory4
    {
        private readonly Mock<ProductRepository> _mockRepository;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory4()
        {
            _mockRepository = new Mock<ProductRepository>(null);
            _service = new ProductBusinessLogicLayer(_mockRepository.Object);
        }

        [Fact]
        public void CreateProduct_ShouldCallRepository_WhenProductIsValid()
        {
            // Arrange
            var newBeer = new LiquidWithAlcohol("Thy Classic", 10m, 100, 33, 25m, 4.6);

            // Act
            _service.CreateProduct(newBeer);

            // Assert
            // Vi verificerer at repository.Create bliver kaldt med et hvilket som helst Product
            _mockRepository.Verify(r => r.Create(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void CreateProduct_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var invalidSnack = new Snack("", 5m, 50, 15m);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _service.CreateProduct(invalidSnack));
            Assert.Equal("Name is required", exception.Message);
            
            // Verificer at repository ALDRIG bliver kaldt, når valideringen fejler
            _mockRepository.Verify(r => r.Create(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void CreateProduct_ShouldThrowException_WhenPriceIsNegative()
        {
            // Arrange
            var cheapBeer = new LiquidWithAlcohol("Free Beer", -1m, 100, 33, 0m, 4.6);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.CreateProduct(cheapBeer));
        }

        [Fact]
        public void CreateLiquidWithAlcohol_ShouldThrowException_WhenAlcoholPercentageIsInvalid()
        {
            // Arrange
            // Alkoholprocent på 110% er ugyldig
            var magicDrink = new LiquidWithAlcohol("Magic", 10m, 10, 33, 50m, 110);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _service.CreateProduct(magicDrink));
            Assert.Equal("Invalid alcohol percentage", exception.Message);
        }

        [Fact]
        public void CreateSnack_ShouldWorkWithValidData()
        {
            // Arrange
            var snack = new Snack("Chips", 5m, 50, 15m);

            // Act
            _service.CreateProduct(snack);

            // Assert
            _mockRepository.Verify(r => r.Create(It.Is<Product>(p => p.Name == "Chips")), Times.Once);
        }
    }
}

