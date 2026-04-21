using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model;
using BusinessLogic.Mappers;
using Data.UnitOfWork;

namespace BusinessLogic.BusinessLogicLayer;

public class ProductBusinessLogicLayer : IProductBusinessLogicLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductBusinessLogicLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    

    public async Task CreateProductAsync(ProductDto product)
    {
        ValidateProduct(product);

        var entity = ProductMapper.Map(product);

        await _unitOfWork.Products.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        await _unitOfWork.Products.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }


    public void ValidateProduct(ProductDto product)
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

    public async Task RegisterSaleAsync(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        var product = await _unitOfWork.Products.GetByIdAsync(productId);

        if (product == null)
            throw new InvalidOperationException("Product not found");

        if (product.StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock");

        product.StockQuantity -= quantity;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RegisterIncomingStockAsync(int productId, int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentException("Invalid quantity");

        var product = await _unitOfWork.Products.GetByIdAsync(productId);

        if (product == null)
            throw new InvalidOperationException("Product not found");

        product.StockQuantity += newQuantity;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductDto product)
    {
        
        ValidateProduct(product);

        var existingProduct = await _unitOfWork.Products.GetByIdAsync(product.Id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        ProductMapper.MapToEntity(product, existingProduct);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ProductDto?> GetProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        return ProductMapper.Map(product);
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return products.Select(ProductMapper.Map).ToList();
    }

    public async Task UpdateMaxStockAsync(int productId, int newMaxStock)
    {
        if (newMaxStock < 0)
        {
            throw new ArgumentException("Invalid stock");
        }

        var product = await _unitOfWork.Products.GetByIdAsync(productId);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.MaxStockQuantity = newMaxStock;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateMinStock(int productId, int newMinStock)
    {
        if (newMinStock < 0)
        {
            throw new ArgumentException("Invalid stock");
        }

        var product = await _unitOfWork.Products.GetByIdAsync(productId);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.MinStockQuantity = newMinStock;
        await _unitOfWork.SaveChangesAsync();
    }




    // Low
    // 2 - kasser albani
    // 2 - kasser tuborg
    // 6 - shakers
    // 2 - falsker vodka
    // Sirup 1 af hver check for volumen
    // min 1/2 kasse af hver sodavand (pepsi max,faxe,pepsi,faxe free,miranda lemon,normal miranda,grøn sodavand,rød sodavand)
    // min 4 falsker tonic vand
    // min 3 kartoner tranebær/apelsin/kakaomælk juice 
    // min 2 sødmælk
    // min 1 spraydåse flødeskum
    // min 1/2 kasse energidrik
    // min 3 poser snakcs (chips,popcorn, peanuts)
    //
    public async Task CheckForLowInventory(int productId)
    {

    }
}