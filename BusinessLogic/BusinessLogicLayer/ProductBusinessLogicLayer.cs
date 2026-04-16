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


    // Creates a new product after validating its details.
    public void CreateProduct(Product product)
    {
        ValidateProduct(product);
        _repository.Create(product);
    }

    // Validates product details before creation or update.
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

    // registrere solgt produkt

    public void SellProduct(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");


        var product = _repository.GetProduct(productId);

        if (product == null)
            throw new NullReferenceException("Product not found");

        if (product.StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock");



        product.StockQuantity -= quantity;
        _repository.Update(product);
    }

    // Register waste for a product, which decreases the stock quantity.
    public void RegisterWaste(int productID, int quantityLost)
    {
        if (quantityLost <= 0)
            throw new ArgumentException("Invalid quantity");

        var product = _repository.GetProduct(productID);

        if (product == null)
            throw new NullReferenceException("Product not found");

        if (product.StockQuantity < quantityLost)
            throw new InvalidOperationException("Not enough stock to register waste");

        product.StockQuantity -= quantityLost;
        _repository.Update(product);
    }

    // Register incoming stock for a product, which increases the stock quantity.
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

    // Update product details such as name, price, etc.
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