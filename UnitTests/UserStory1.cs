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
        private readonly IProductBusinessLogicLayer _service;

        public UserStory1(ITestOutputHelper testOutputHelper)
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockSalesRepository = new Mock<ISalesRepository>();
            
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Sales).Returns(_mockSalesRepository.Object);

            _service = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task RegisterSale_ReducesStock()
        {
            var product1 = new Liquid { Id = 1, Name = "Beer", StockQuantity = 10};
            var product2 = new Liquid { Id = 2, Name = "Vodka", StockQuantity = 10, VolumeCl = 50 };
            var product3 = new Snack { Id = 3, Name = "Chips", StockQuantity = 5 };

            // Set up the mock repository to return specific products based on ID
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(2)).ReturnsAsync(product2);
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(3)).ReturnsAsync(product3);
            
            var productIds = new List<int> { 1, 1, 2, 3 };
            
            await _service.RegisterSaleAsync(productIds);
            
            Assert.Equal(8, product2.StockQuantity); // 10 - 2
            Assert.Equal(46, product1.VolumeCl); // 50 - 4
            Assert.Equal(4, product2.StockQuantity); // 5 - 1
            
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenStockIsTooLow()
        {
            var product1 = new Liquid { Id = 1, Name = "Beer", StockQuantity = 1 };
            var product2 = new Snack { Id = 2, Name = "Chips", StockQuantity = 5 };

            // Set up the mock repository to return specific products based on ID
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(2)).ReturnsAsync(product2);

            var productIds = new List<int> { 1, 1, 2 };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(productIds));

            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterSale_ThrowsArgumentException_WhenQuantityIsZeroOrLess()
        {
            var product1 = new Liquid { Id = 1, Name = "Beer", StockQuantity = 0 };
            var product2 = new Snack { Id = 2, Name = "Soda", StockQuantity = 0 };

            // Set up the mock repository to return specific products based on ID
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(2)).ReturnsAsync(product2);

            var productIds = new List<int> { 1, 1, 2 };
            
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(productIds));

            _mockProductRepository.Verify(repository => repository.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenProductDoesNotExist()
        {
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(999)).ReturnsAsync((Product)null);
            
            var productIds = new List<int> { 999 };
            
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(productIds));
            // Verify that SaveChangesAsync was NEVER called because the operation failed
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Never);
        }
    }
}
