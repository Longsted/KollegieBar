using Data.Repositories;
using DataTransferObject.Model;

namespace BusinessLogic.BusinessLogicLayer;

public class ProductBusinessLogicLayer
{
    private readonly ProductRepository _repository;

    public ProductBusinessLogicLayer(ProductRepository repository)
    {
        _repository = repository;
    }

 

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            throw new NullReferenceException("Product not found");
        }
        
        
    }

    public void CreateProduct(Product product)
    {
        ValidateProduct(product);
        _repository.Create(product);
    }

    public void ValidateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Name is required");

        if (product.CostPrice < 0)
            throw new ArgumentException("Invalid price");

        if (product.StockQuantity < 0)
            throw new ArgumentException("Invalid stock");

        if (product is LiquidWithAlcohol alcohol)
        {
            if (alcohol.AlcoholPercentage < 0 || alcohol.AlcoholPercentage > 100)
                throw new ArgumentException("Invalid alcohol percentage");
        }
    }

    public void RegisterSale(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");
        
        var product = _repository.GetProduct(productId);

        if (product == null)
            throw new NullReferenceException("Product not found");

        if (product.StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock");

        var newTotalStock = product.StockQuantity -= quantity;
        _repository.UpdateStock(productId, newTotalStock);
    }

    public void RegisterIncomingStock(int productId, int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentException("Invalid quantity");

        var product = _repository.GetProduct(productId);

        if (product == null)
            throw new ArgumentException("Product not found");

        var newTotalStock = product.StockQuantity += newQuantity;
        _repository.UpdateStock(productId, newTotalStock);
    }

    public void UpdateProduct(Product updatedProduct)
    {
        if (updatedProduct == null)
        {
            throw new ArgumentNullException(nameof(updatedProduct));
        }

        ValidateProduct(updatedProduct);
        
        var existingProduct = _repository.GetProduct(updatedProduct.Id);
        if (existingProduct == null)
        {
            throw new ArgumentNullException("existingProduct", "Product not found");
        }
        _repository.Update(updatedProduct);
    }
}