using Data.Model;

namespace BusinessLogic.InterfaceBusiness;

using DataTransferObject.Model;

public interface IProductBusinessLogicLayer
{
    Task<ProductDataTransferObject?> GetProductAsync(int id);
    
    Task<List<ProductDataTransferObject>> GetAllProductsAsync();
    
    Task CreateProductAsync(ProductDataTransferObject product);
    
    Task UpdateProductAsync(ProductDataTransferObject product);
    
    Task DeleteProductAsync(int id);
    
    Task RegisterSaleAsync(List<(int productId,int quantity)> items);
    
    Task RegisterIncomingStockAsync(int productId, int quantity);

    Task UpdateMaxStockAsync(int productId, int newMaxStock);
    
    Task UpdateMinStock(int productId, int newMinStock);
}