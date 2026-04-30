using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Interfaces;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserStory5
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly IProductBusinessLogicLayer _service;

        public UserStory5()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            
            _service = new ProductBusinessLogicLayer(_mockUnitOfWork.Object);
            
            _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);
        }
        
        [Fact]
        public void BoardMember_ShouldReceive_ListOfLowInventoryItems()
        {
            var products = new List<ProductDataTransferObject>
            {
                new LiquidDataTransferObject { Id = 1, Name = "Beer", StockQuantity = 50, AlcoholPercentage = 5, MinStockQuantity = 30},
                new SnackDataTransferObject { Id = 3, Name = "Popcorn", StockQuantity = 2, MinStockQuantity = 4},
                new LiquidDataTransferObject { Id = 1, Name = "Shaker", StockQuantity = 10, AlcoholPercentage = 5, MinStockQuantity = 9},
            };

            var lowInventory = _service.GetLowInventoryProducts(products);
            
            Assert.NotNull(lowInventory);
            
            Assert.Single(lowInventory);
            
            Assert.Contains(lowInventory, p => p.Name == "Popcorn");

            Assert.DoesNotContain(lowInventory, p => p.Name == "Beer");
            Assert.DoesNotContain(lowInventory, p => p.Name == "Shaker");
        }
        
        [Fact]
        public void GetLowInventoryProducts_ShouldReturnEmptyList_WhenInputIsEmpty()
        {
            var products = new List<ProductDataTransferObject>();
            
            var result = _service.GetLowInventoryProducts(products);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void GetLowInventoryProducts_ShouldIncludeItem_WhenStockIsExactlyMinStock()
        {
            var products = new List<ProductDataTransferObject> 
            { 
                new SnackDataTransferObject { Name = "Exact", StockQuantity = 10, MinStockQuantity = 10 } 
            };
            
            var result = _service.GetLowInventoryProducts(products);
            
            Assert.Single(result);
        }
        
        [Fact]
        public void GetLowInventoryProducts_ShouldReturnEmpty_WhenAllStockIsHigh()
        {
            var products = new List<ProductDataTransferObject> 
            { 
                new SnackDataTransferObject { Name = "Full", StockQuantity = 100, MinStockQuantity = 10 } 
            };
            
            var result = _service.GetLowInventoryProducts(products);

            Assert.Empty(result);
        }
    }
}
