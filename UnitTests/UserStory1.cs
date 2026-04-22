using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory1
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory1()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _service = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task RegisterSale_ReducesStock()
        {
            var product = new LiquidDataTransferObject { Id = 1, Name = "Beer", StockQuantity = 10 };

            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product);

            await _service.RegisterSaleAsync(1, 2);

            Assert.Equal(8, product.StockQuantity);
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenStockIsTooLow()
        {
            var product = new LiquidDataTransferObject { Id = 1, StockQuantity = 1 };

            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(1, 5));

            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterSale_ThrowsArgumentException_WhenQuantityIsZeroOrLess()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.RegisterSaleAsync(1, 0));

            _mockProductRepository.Verify(repository => repository.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task RegisterSale_ThrowsException_WhenProductDoesNotExist()
        {
            _mockProductRepository.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Product)null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(999, 1));
        }
    }
}
