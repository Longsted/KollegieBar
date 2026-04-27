namespace BusinessLogic.InterfaceBusiness;

using DataTransferObject.Model;

public interface IProductBusinessLogicLayer
{
    Task<ProductDataTransferObject?> GetProductAsync(int id);

    Task<List<ProductDataTransferObject>> GetAllProductsAsync();

    Task CreateProductAsync(ProductDataTransferObject product);

    Task UpdateProductAsync(ProductDataTransferObject product);

    Task DeleteProductAsync(int id);

    Task RegisterIncomingStockAsync(int productId, int quantity);

    Task RegisterWaste(List<int> productIds);

    Task UpdateMaxStockAsync(int productId, int newMaxStock);

    Task UpdateMinStock(int productId, int newMinStock);


}