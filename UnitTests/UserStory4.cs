using BusinessLogic.BusinessLogicLayer;
using Data.Repositories;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory4
    {
        [Fact]
        public void CreateLiquidWithAlcohol_ShouldCallRepository_WhenCalled()
        {
            // 1. Arrange (Opsætning)
            // Vi "mocker" repository'et så vi ikke behøver en rigtig database
            var mockRepository = new Mock<ProductRepository>(null); 
            var service = new ProductBusinessLogicLayer(mockRepository.Object);
            
            var newBeer = new LiquidWithAlcohol("Thy Classic", 10m, 100, 33, 25m, 4.6);

            // 2. Act (Udfør handlingen)
            service.CreateLiquidWithAlcohol(newBeer);

            // 3. Assert (Verificér at BLL har sendt varen videre til Repository)
            mockRepository.Verify(r => r.CreateLiquidWithAlcohol(It.IsAny<LiquidWithAlcohol>()), Times.Once);
        }

        [Fact]
        public void CreateSnack_ShouldStoreCorrectData()
        {
            // Arrange
            var mockRepository = new Mock<ProductRepository>(null);
            var service = new ProductBusinessLogicLayer(mockRepository.Object);
            var snack = new Snack("Chips", 5m, 50, 15m);

            // Act
            service.CreateSnack(snack);

            // Assert
            mockRepository.Verify(r => r.CreateSnack(It.Is<Snack>(s => s.Name == "Chips")), Times.Once);
        }
    }
}

