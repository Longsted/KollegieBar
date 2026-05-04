using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace UnitTests
{
    public class UserStory10
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IDrinkRepository> _mockDrinkRepository;
        private readonly ProductBusinessLogicLayer _productService;

        public UserStory10()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockDrinkRepository = new Mock<IDrinkRepository>();

            _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(u => u.Drinks).Returns(_mockDrinkRepository.Object);

            _productService = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task RegisterWaste_ValidProductsAndDrinks_ReducesStockCorrectly()
        {
            var beer = new Liquid { Id = 1, Name = "Beer Bottle", StockQuantity = 10 };
            var chips = new Snack { Id = 2, Name = "Chips", StockQuantity = 5 };
            var rum = new Liquid { Id = 3, Name = "Rum", StockQuantity = 8 };

            var drink = new Drink
            {
                Id = 10,
                Name = "Rum & Cola",
                Ingredients = new List<Liquid> { rum }
            };

            _mockProductRepository
                .Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { beer, chips });

            _mockDrinkRepository
                .Setup(r => r.GetDrinksWithIngredientsAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(new List<Drink> { drink });

            var productIds = new List<int> { 1, 1, 2 };
            var drinkIds = new List<int> { 10 };

            await _productService.RegisterWaste(productIds, drinkIds);

            Assert.Equal(8, beer.StockQuantity);
            Assert.Equal(4, chips.StockQuantity);
            Assert.Equal(7, rum.StockQuantity);

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterWaste_ProductNotFound_ThrowsInvalidOperationException()
        {
            var beer = new Liquid { Id = 1, Name = "Beer Bottle", StockQuantity = 10 };

            _mockProductRepository
                .Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { beer });

            var productIds = new List<int> { 1, 2 };
            var drinkIds = new List<int>();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _productService.RegisterWaste(productIds, drinkIds));

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterWaste_InsufficientStock_ThrowsInvalidOperationException()
        {
            var beer = new Liquid { Id = 1, Name = "Beer Bottle", StockQuantity = 1 };

            _mockProductRepository
                .Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { beer });

            var productIds = new List<int> { 1, 1 };
            var drinkIds = new List<int>();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _productService.RegisterWaste(productIds, drinkIds));

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterWaste_InsufficientDrinkIngredientStock_ThrowsInvalidOperationException()
        {
            var rum = new Liquid { Id = 3, Name = "Rum", StockQuantity = 0 };
            var drink = new Drink { Id = 10, Name = "Rum & Cola", Ingredients = new List<Liquid> { rum } };

            _mockProductRepository
                .Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product>());

            _mockDrinkRepository
                .Setup(r => r.GetDrinksWithIngredientsAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(new List<Drink> { drink });

            var productIds = new List<int>();
            var drinkIds = new List<int> { 10 };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _productService.RegisterWaste(productIds, drinkIds));

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}

