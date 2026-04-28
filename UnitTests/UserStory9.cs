using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{

    public class UserStory9
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IDrinkRepository> _mockDrinkRepository;
        private readonly DrinksBusinessLayer _drinksService;

        public UserStory9()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockDrinkRepository = new Mock<IDrinkRepository>();

            _mockUnitOfWork.Setup(u => u.Drinks).Returns(_mockDrinkRepository.Object);

            _drinksService = new DrinksBusinessLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task UpdateDrinkAsync_ValidDrink_UpdatesDrink()
        {
            var existingDrink = new Drink
            {
                Id = 1,
                Name = "Old Drink",
                CostPrice = 25
            };

            var updatedDrink = new DrinkDataTransferObject
            {
                Id = 1,
                Name = "Special Drink",
                CostPrice = 35,
                Ingredients = new List<DrinkIngredientDataTransferObject>()
            };

            _mockDrinkRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(existingDrink);

            await _drinksService.UpdateDrinkAsync(updatedDrink);

            Assert.Equal("Special Drink", existingDrink.Name);
            Assert.Equal(35, existingDrink.CostPrice);

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateDrinkAsync_InvalidId_ThrowsArgumentException()
        {
            var drink = new DrinkDataTransferObject
            {
                Id = 0,
                Name = "Special Drink",
                CostPrice = 35
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _drinksService.UpdateDrinkAsync(drink));

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);

        }

        [Fact]
        public async Task UpdateDrinkAsync_DrinkDoesNotExist_ThrowsInvalidOperationException()
        {
            var drink = new DrinkDataTransferObject()
            {
                Id = 1,
                Name = "Special Drink",
                CostPrice = 35
            };

            _mockDrinkRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Drink?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _drinksService.UpdateDrinkAsync(drink));

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }


        [Fact]
        public async Task UpdateDrinkAsync_WithIngredients_ThrowsInvalidOperationException()
        {
            var drink = new DrinkDataTransferObject
            {
                Id = 1,
                Name = "Special Drink",
                CostPrice = 35,
                Ingredients = new List<DrinkIngredientDataTransferObject>
                {
                    new DrinkIngredientDataTransferObject
                    {
                        LiquidProductId = 2
                    }
                }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _drinksService.UpdateDrinkAsync(drink));

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}
