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
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductBusinessLogicLayer _service;

        public UserStory3()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockProductRepo = new Mock<IProductRepository>();

            _mockUow.Setup(u => u.Products).Returns(_mockProductRepo.Object);

            _service = new ProductBusinessLogicLayer(_mockUow.Object);
        }

        [Fact]
        public async Task RegisterIncomingStock_ShouldUpdateStock_WhenProductExists()
        {
            var existingProduct = new Snack { Id = 1, StockQuantity = 10 };

            _mockProductRepo.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(existingProduct);

            await _service.RegisterIncomingStockAsync(1, 30);

            Assert.Equal(40, existingProduct.StockQuantity);
            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterIncomingStock_ShouldThrowException_WhenProductNotFound()
        {
            _mockProductRepo.Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Product)null);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.RegisterIncomingStockAsync(999, 10));

            Assert.Equal("Product not found", ex.Message);

            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RegisterIncomingStock_ShouldThrowException_WhenQuantityIsNegative()
        {
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.RegisterIncomingStockAsync(1, -5));

            Assert.Equal("Invalid quantity", ex.Message);

            _mockProductRepo.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}