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
}