using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class UserStory1
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ISalesRepository> _mockSalesRepository; 
        private readonly ISalesBusinessLayer _salesService;
        private readonly Mock<IDrinkRepository> _mockDrinkRepository;

        public UserStory1()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockDrinkRepository = new Mock<IDrinkRepository>();


            // Setup Unit of Work to return of all of the repositories
            _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(u => u.Sales).Returns(_mockSalesRepository.Object);
            _mockUnitOfWork.Setup(u => u.Drinks).Returns(_mockDrinkRepository.Object); 

            _salesService = new SalesBusinessLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task RegisterSale_ValidProductsAndDrinks_ReducesStockCorrectly()
        {
            // Arrange
            var beer = new Liquid { Id = 1, Name = "Beer", StockQuantity = 10, AlcoholPercentage = 5 };
            var chips = new Snack { Id = 3, Name = "Chips", StockQuantity = 5 };

            // A mixer (soda) with deposit (pant), which is part of a drink
            var tonic = new Liquid { Id = 4, Name = "Tonic", StockQuantity = 10, Pant = Pant.B };
            var ginTonic = new Drink
            {
                Id = 10,
                Name = "Gin & Tonic",
                Ingredients = new List<Liquid> {
                    tonic
                }
            };

            // Mocking bulk fetch of products
            _mockProductRepository.Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { beer, chips });

            // Mocking bulk fetch of drinks
            _mockDrinkRepository.Setup(r => r.GetDrinksWithIngredientsAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(new List<Drink> { ginTonic });

            var productIds = new List<int> { 1, 1, 3 }; // 2 beers, 1 chips
            var drinkIds = new List<int> { 10 };        // 1 G&T (should deduct 1 tonic due to deposit/pant)

            // Act
            await _salesService.RegisterSaleAsync(productIds, drinkIds);

            // Assert
            Assert.Equal(8, beer.StockQuantity);  // 10 - 2
            Assert.Equal(4, chips.StockQuantity); // 5 - 1
            Assert.Equal(9, tonic.StockQuantity); // 10 - 1 (Deducted due to deposit logic)

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

            // Verify that 4 sale records were created in total (2 beers + 1 chips + 1 drink)
            _mockSalesRepository.Verify(r => r.AddRangeAsync(It.Is<List<Sale>>(list => list.Count == 4)), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_InsufficientStock_ThrowsInvalidOperationException()
        {
            // Arrange
            var snack = new Snack { Id = 1, Name = "Chocolate", StockQuantity = 1 };

            // Mocking that the chocolate is found in the database
            _mockProductRepository.Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { snack });

            var productIds = new List<int> { 1, 1 }; // Ordering 2, but only 1 in stock
            var drinkIds = new List<int>();          // No drinks in this purchase

            // Act & Assert
            // We expect an error, and that SaveChangesAsync is NEVER called
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _salesService.RegisterSaleAsync(productIds, drinkIds));

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterSale_StrongAlcoholWithNoStock_ThrowsException()
        {
            // Arrange
            // Creating a Vodka (40%) which is empty (StockQuantity = 0)
            var vodka = new Liquid
            {
                Id = 2,
                Name = "Vodka",
                StockQuantity = 0,
                AlcoholPercentage = 40
            };

            // Mocking bulk fetch. Even though we only request one ID, 
            // RegisterSaleAsync now uses GetWhereAsync.
            _mockProductRepository.Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { vodka });

            var productIds = new List<int> { 2 };
            var drinkIds = new List<int>(); // No drinks in this purchase

            // Act & Assert
            // We expect an InvalidOperationException because AlcoholPercentage >= 16 and Stock is 0
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _salesService.RegisterSaleAsync(productIds, drinkIds));

            // Verify that we never attempted to save to the database
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}