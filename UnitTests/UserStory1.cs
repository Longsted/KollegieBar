using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory1
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory1()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockProductRepo = new Mock<IProductRepository>();

            _mockUow.Setup(u => u.Products).Returns(_mockProductRepo.Object);

            _service = new ProductBusinessLogicLayer(_mockUow.Object);
        }

        [Fact]
        public async Task RegisterSale_ReducesStock()
        {
            var product = new LiquidWithAlcohol { Id = 1, Name = "Beer", StockQuantity = 10 };

            _mockProductRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(product);

            await _service.RegisterSaleAsync(1, 2);

            Assert.Equal(8, product.StockQuantity);
            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenStockIsTooLow()
        {
            var product = new LiquidWithAlcohol { Id = 1, StockQuantity = 1 };

            _mockProductRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(product);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.RegisterSaleAsync(1, 5));

            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterSale_ThrowsArgumentException_WhenQuantityIsZeroOrLess()
        {
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.RegisterSaleAsync(1, 0));

            _mockProductRepo.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenProductDoesNotExist()
        {
            _mockProductRepo.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Product)null);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.RegisterSaleAsync(999, 1));
        }
    }
}
