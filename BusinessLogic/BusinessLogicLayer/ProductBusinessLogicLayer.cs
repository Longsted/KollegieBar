using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model;
using BusinessLogic.Mappers;
using Data.UnitOfWork;

namespace BusinessLogic.BusinessLogicLayer;

public class ProductBusinessLogicLayer : IProductBusinessLogicLayer
{
    private readonly IUnitOfWork _uow;

    public ProductBusinessLogicLayer(IUnitOfWork uow)
    {
        _uow = uow;
    }
    

    public async Task CreateProductAsync(ProductDto dto)
    {
        ValidateProduct(dto);

        var entity = ProductMapper.Map(dto);

        await _uow.Products.AddAsync(entity);
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _uow.Products.GetByIdAsync(id);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        await _uow.Products.DeleteAsync(product);
        await _uow.SaveChangesAsync();
    }


    public void ValidateProduct(ProductDto productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.Name))
            throw new ArgumentException("Name is required");

        if (productDto.CostPrice < 0)
            throw new ArgumentException("Invalid price");

        if (productDto.StockQuantity < 0)
            throw new ArgumentException("Invalid stock");

        if (productDto is LiquidWithAlcohol alcohol)
        {
            if (alcohol.AlcoholPercentage < 0 || alcohol.AlcoholPercentage > 100)
                throw new ArgumentException("Invalid alcohol percentage");
        }
    }

    public async Task RegisterSaleAsync(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        var product = await _uow.Products.GetByIdAsync(productId);

        if (product == null)
            throw new InvalidOperationException("Product not found");

        if (product.StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock");

        product.StockQuantity -= quantity;
        await _uow.SaveChangesAsync();
    }

    public async Task RegisterIncomingStockAsync(int productId, int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentException("Invalid quantity");

        var product = await _uow.Products.GetByIdAsync(productId);

        if (product == null)
            throw new InvalidOperationException("Product not found");

        product.StockQuantity += newQuantity;
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductDto dto)
    {
        
        ValidateProduct(dto);

        var existingProduct = await _uow.Products.GetByIdAsync(dto.Id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        ProductMapper.MapToEntity(dto, existingProduct);

        await _uow.SaveChangesAsync();
    }

    public async Task<ProductDto?> GetProductAsync(int id)
    {
        var product = await _uow.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        return ProductMapper.Map(product);
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await _uow.Products.GetAllAsync();
        return products.Select(ProductMapper.Map).ToList();
    }

    public async Task UpdateMaxStockAsync(int productId, int newMaxStock)
    {
        if (newMaxStock < 0)
        {
            throw new ArgumentException("Invalid stock");
        }

        var product = await _uow.Products.GetByIdAsync(productId);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.MaxStockQuantity = newMaxStock;

        await _uow.SaveChangesAsync();
    }

    public async Task UpdateMinStock(int productId, int newMinStock)
    {
        if (newMinStock < 0)
        {
            throw new ArgumentException("Invalid stock");
        }

        var product = await _uow.Products.GetByIdAsync(productId);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.MinStockQuantity = newMinStock;
        await _uow.SaveChangesAsync();
    }
}