using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model;
using BusinessLogic.Mappers;
using Data.Model;
using Data.UnitOfWork;
using Sale = Data.Model.Sale;

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

    public async Task RegisterSaleAsync(List<int>productIds)
    {
        var grouped = productIds.GroupBy(id => id).
            Select(g => new { productId = g.Key,
                                            quantity = g.Count() });
        
        var transactionId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        var allSales = new List <Sale>();
        foreach (var item in grouped)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            switch (product)
            {
                case Data.Model.Snack snack:
                    HandleSnackSale(snack, item.quantity);
                    break;
                case Data.Model.Liquid liquid:
                    HandleAlcoholSale(liquid, item.quantity);
                    break;
                default:

                    throw new NotSupportedException($"unknown type product {product.GetType().Name}");

            }



            var sales = CreateSales(product, item.quantity, transactionId, now);
            allSales.AddRange(sales);
            
        }
        await _unitOfWork.Sales.AddRangeAsync(allSales);
        await _unitOfWork.SaveChangesAsync();
    }

    private void HandleSnackSale(Data.Model.Snack snack, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Invalid quantity");
        }

        if (snack.StockQuantity < quantity)
        {
            throw new InvalidOperationException("Not enough stock");
        }

        snack.StockQuantity -= quantity;
    }

    private void HandleAlcoholSale(Data.Model.Liquid alcohol, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Invalid quantity");
        }

        var removeClFromBottle = quantity * 20;
        if (alcohol.VolumeCl < removeClFromBottle && alcohol.StockQuantity == 0)
        {
            throw new InvalidOperationException("Not enough stock");
        }

        alcohol.VolumeCl -= removeClFromBottle;
    }

    private List<Sale> CreateSales(Data.Model.Product product, int quantity, Guid transactionId,
        DateTime now)
    {
        var sales = new List<Sale>();

        for (int i = 0; i < quantity; i++)
        {
            var sale = new Sale(product.CostPrice, now, transactionId, product);
            sales.Add(sale);
        }

        return sales;
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

    public async Task RegisterWaste(int productId, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Invalid quantity");
        var product = await _unitOfWork.Products.GetByIdAsync(productId);
        if (product == null)
            throw new InvalidOperationException("Product not found");
        if (product.StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock to register waste");
        product.StockQuantity -= quantity;
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