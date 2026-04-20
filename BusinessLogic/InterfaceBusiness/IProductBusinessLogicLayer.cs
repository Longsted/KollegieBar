using Data.Model;

namespace BusinessLogic.InterfaceBusiness;

using DataTransferObject.Model;

public interface IProductBusinessLogicLayer
{
    Task<ProductDto?> GetProductAsync(int id);
    
    Task<List<ProductDto>> GetAllProductsAsync();
    
    Task CreateProductAsync(ProductDto productDto);
    
    Task UpdateProductAsync(ProductDto productDto);
    
    Task DeleteProductAsync(int id);
    
    Task RegisterSaleAsync(int productId, int quantity);
    
    Task RegisterIncomingStockAsync(int productId, int quantity);

    Task UpdateMaxStockAsync(int productId, int newMaxStock);
    
    Task UpdateMinStock(int productId, int newMinStock);
}