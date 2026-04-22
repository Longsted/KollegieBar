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
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly IProductBusinessLogicLayer _service;
        private readonly Mock<ISalesRepository> _mockSalesRepository;

        public UserStory1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _service = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
            _mockSalesRepository = new Mock<ISalesRepository>();
        }

        [Fact]
        public async Task RegisterSale_ReducesStock()
        {
            var product1 = new Liquid { Id = 1, Name = "Beer", StockQuantity = 10 };
            var product2 = new Snack { Id = 2, Name = "Soda", StockQuantity = 5 };

            // Set up the mock repository to return specific products based on ID
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(2)).ReturnsAsync(product2);
            
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Sales).Returns(_mockSalesRepository.Object);

            var items = new List<(int productId, int quantity)>
            {
                (1, 2), // Sale: 2 Beers
                (2, 1)  // Sale: 1 Soda
            };
            
            await _service.RegisterSaleAsync(items);
            
            Assert.Equal(8, product1.StockQuantity); // 10 - 2
            Assert.Equal(4, product2.StockQuantity); // 5 - 1
            
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenStockIsTooLow()
        {
            var product1 = new Liquid { Id = 1, Name = "Beer", StockQuantity = 1 };
            var product2 = new Snack { Id = 2, Name = "Cider", StockQuantity = 5 };

            // Set up the mock repository to return specific products based on ID
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(2)).ReturnsAsync(product2);

            var items = new List<(int productId, int quantity)>
            {
                (1, 2), // Sale: 2 Beers
                (2, 1)  // Sale: 1 Soda
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(items));

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

            var items = new List<(int productId, int quantity)>
            {
                (1, 2), // Sale: 2 Beers
                (2, 1)  // Sale: 1 Soda
            };
            
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(items));

            _mockProductRepository.Verify(repository => repository.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenProductDoesNotExist()
        {
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(999)).ReturnsAsync((Product)null);
            
            var items = new List<(int productId, int quantity)> 
            { 
                (999, 1) 
            };

            // Verify that SaveChangesAsync was NEVER called because the operation failed
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Never);
        }
    }
}
