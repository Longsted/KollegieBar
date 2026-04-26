using BusinessLogic.InterfaceBusiness;
using BusinessLogic.Mappers;
using Data.UnitOfWork;
using DataTransferObject.Model;

namespace BusinessLogic.BusinessLogicLayer;

public class ProductBusinessLogicLayer : IProductBusinessLogicLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductBusinessLogicLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task CreateProductAsync(ProductDataTransferObject product)
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


    public void ValidateProduct(ProductDataTransferObject product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Name is required");

        if (product.CostPrice < 0)
            throw new ArgumentException("Invalid price");

        if (product.StockQuantity < 0)
            throw new ArgumentException("Invalid stock");

        if (product is LiquidDataTransferObject alcohol)
        {
            if (alcohol.AlcoholPercentage < 0 || alcohol.AlcoholPercentage > 100)
                throw new ArgumentException("Invalid alcohol percentage");
        }
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

    public async Task UpdateProductAsync(ProductDataTransferObject product)
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

    public async Task<ProductDataTransferObject?> GetProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        return ProductMapper.Map(product);
    }

    public async Task<List<ProductDataTransferObject>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return products.Select(ProductMapper.Map).ToList();
    }

    //This method is used to register waste of a products, it reduces the stock by 1 for each product id in the list.
    //Could save the changes to a waste log if needed, but for now it just updates the stock quantity.
    public async Task RegisterWaste(List<int> productIds)
    {
        var uniqueIds = productIds.Distinct().ToList();
        var products = await _unitOfWork.Products.GetWhereAsync(p => uniqueIds.Contains(p.Id));


        if (products.Count != uniqueIds.Count)
        {
            throw new InvalidOperationException("One or more products were not found.");
        }

        var wasteCounts = productIds.GroupBy(id => id)
                                    .ToDictionary(g => g.Key, g => g.Count());

        foreach (var product in products)
        {
            int amountToRemove = wasteCounts[product.Id];

            if (product.StockQuantity < amountToRemove)
            {
                throw new InvalidOperationException($"Not enough stock for product {product.Id}");
            }

            product.StockQuantity -= amountToRemove;
        }
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task RegisterWasteVolume(int productId, int volume)
    {
        if (volume < 0)
            throw new ArgumentException("Invalid quantity");

        var product = await _unitOfWork.Products.GetByIdAsync(productId);

        if (product == null)
            throw new InvalidOperationException("Product not found");

        if (product is not Data.Model.Liquid liquid)
            throw new InvalidOperationException("Product is not a liquid");

        if (liquid.VolumeCl < volume)
            throw new InvalidOperationException("Not enough volume to register waste");

        liquid.VolumeCl -= volume;
        await _unitOfWork.SaveChangesAsync();
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