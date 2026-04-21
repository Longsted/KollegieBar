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

        if (product is LiquidDataTransferObject alcohol)
        {
            if (alcohol.AlcoholPercentage < 0 || alcohol.AlcoholPercentage > 100)
                throw new ArgumentException("Invalid alcohol percentage");
        }
    }

    public async Task RegisterSaleAsync(List<(int productId, int quantity)> items)
    {
        var transactionId = new Guid();
        var now = DateTime.UtcNow;

        foreach (var item in items)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            switch ( product)
            {
                case DataTransferObject.Model alcohol :
                    
                    
            }
        }

       
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

    private void HandleAlcoholSale(DataTransferObject.Model.LiquidProductDto alcohol, int quantity)
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

    private List<Sale> CreateSales(ProductDto product, int quantity, Guid transactionId, DateTime now)
    {
        var sales = new List<Sale>();

        for (int i = 0; i < quantity; i++)
        {
            var sale = new Sale(product.CostPrice, now, transactionId, product.Id);
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
}