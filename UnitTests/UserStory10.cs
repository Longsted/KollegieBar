using System.Linq.Expressions;
using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using Moq;
using Xunit;

namespace UnitTests
{
  
    public class UserStory10
    {
        
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductBusinessLogicLayer _productService;


        public UserStory10()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            
            _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);
            
            _productService = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
        }
        
        [Fact]
        public async Task RegisterWaste_ValidProducts_ReducesStockCorrectly()
        {
            var beer = new Liquid
            {
                Id = 1,
                Name = "Beer  Bottle",
                StockQuantity = 10
            };

            var chips = new Snack
            {
                Id = 2,
                Name = "Chips",
                StockQuantity = 5
            };

            _mockProductRepository.Setup(r => r
                .GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { beer, chips });
            
            var productIds = new List<int> { 1, 1, 2 };

            await _productService.RegisterWaste(productIds);
            
            Assert.Equal(8, beer.StockQuantity);
            Assert.Equal(4, chips.StockQuantity);
            
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
        
        [Fact]
        public async Task RegisterWaste_ProductNotFound_ThrowsInvalidOperationException()
        {
            var beer = new Liquid
            {
                Id = 1,
                Name = "Beer  Bottle",
                StockQuantity = 10
            };

            _mockProductRepository
                .Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { beer });

            var productIds = new List<int> { 1, 2 };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _productService.RegisterWaste(productIds));
            
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
        
        
        [Fact]
        public async Task RegisterWaste_InsufficientStock_ThrowsInvalidOperationException()
        {
            var beer = new Liquid
            {
                Id = 1,
                Name = "Beer  Bottle",
                StockQuantity = 1
            };
            
            _mockProductRepository
                .Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { beer });

            var productIds = new List<int> { 1, 1};
            
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _productService.RegisterWaste(productIds));
            
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
        
        
    }
    
}
