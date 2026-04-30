using Xunit;
using System.Collections.Generic;
using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Moq;

namespace UnitTests
{
    public class UserStory2
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ISalesRepository> _mockSalesRepository; 
        private readonly ISalesBusinessLayer _salesService;
        private readonly Mock<IDrinkRepository> _mockDrinkRepository;

        public UserStory2()
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
        public async Task GetMostPopularProducts_ShouldReturnOrderedListBySales()
        {
            var product1 = new LiquidDataTransferObject("Beer", 15, 100, 33, 4.6) { Id = 1 };
            var product2 = new LiquidDataTransferObject("Soda", 10, 100, 33, 0.0) { Id = 2 };
            
            var transactionId = Guid.NewGuid();
            var sales = new List<SaleDataTransferObject>
            {
                new SaleDataTransferObject(DateTime.Now, transactionId, product1),
                new SaleDataTransferObject(DateTime.Now, transactionId, product2), 
                new SaleDataTransferObject(DateTime.Now, transactionId, product2)  
            };

            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(sales);

            var result = await _salesService.GetMostPopularProductsAsync(2);
            
            Assert.Equal(2, result.Count);
            Assert.Equal("Soda", result[0].Name);
            Assert.Equal("Beer", result[1].Name);
        }
        
        
        
    }
}
