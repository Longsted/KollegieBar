using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
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

        public UserStory1()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockSalesRepository = new Mock<ISalesRepository>();
            
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Sales).Returns(_mockSalesRepository.Object);

            _salesService = new SalesBusinessLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task RegisterSale_ValidProducts_ReducesStockCorrectly()
        {
            // Arrange
            var beer = new Liquid { Id = 1, Name = "Beer", StockQuantity = 10, AlcoholPercentage = 5 };
            var chips = new Snack { Id = 3, Name = "Chips", StockQuantity = 5 };

            _mockProductRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(beer);
            _mockProductRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(chips);
            
            var productIds = new List<int> { 1, 1, 3 }; // 2 øl, 1 chips
            
            // Act
            await _salesService.RegisterSaleAsync(productIds);
            
            // Assert
            Assert.Equal(8, beer.StockQuantity); // 10 - 2
            Assert.Equal(4, chips.StockQuantity); // 5 - 1
            
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
            _mockSalesRepository.Verify(r => r.AddRangeAsync(It.Is<List<Sale>>(list => list.Count == 3)), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_InsufficientStock_ThrowsInvalidOperationException()
        {
            // Arrange
            var snack = new Snack { Id = 1, Name = "Chocolate", StockQuantity = 1 };
            _mockProductRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(snack);

            var productIds = new List<int> { 1, 1 }; // Bestiller 2, har kun 1

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _salesService.RegisterSaleAsync(productIds));
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterSale_StrongAlcoholWithNoStock_ThrowsException()
        {
            // Test af din logik for alkohol >= 16%
            var vodka = new Liquid { Id = 2, Name = "Vodka", StockQuantity = 0, AlcoholPercentage = 40 };
            _mockProductRepository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(vodka);

            var productIds = new List<int> { 2 };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _salesService.RegisterSaleAsync(productIds));
        }
    }
}
