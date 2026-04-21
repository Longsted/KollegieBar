using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory3
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory3()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Products).Returns(_mockProductRepository.Object);
            _service = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task RegisterIncomingStock_ShouldUpdateStock_WhenProductExists()
        {
            var product = new Snack { Id = 1, StockQuantity = 10 };

            _mockProductRepository.Setup(repository => repository.GetByIdAsync(1)).ReturnsAsync(product);

            await _service.RegisterIncomingStockAsync(1, 30);

            Assert.Equal(40, product.StockQuantity);
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterIncomingStock_ShouldThrowException_WhenProductNotFound()
        {
            _mockProductRepository.Setup(repository => repository.GetByIdAsync(999))
                .ReturnsAsync((Product)null);

            var product = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.RegisterIncomingStockAsync(999, 10));

            Assert.Equal("Product not found", product.Message);

            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterIncomingStock_ShouldThrowException_WhenQuantityIsNegative()
        {
            var product = await Assert.ThrowsAsync<ArgumentException>(() => _service.RegisterIncomingStockAsync(1, -5));

            Assert.Equal("Invalid quantity", product.Message);

            _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}