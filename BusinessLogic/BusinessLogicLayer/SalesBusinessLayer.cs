using BusinessLogic.InterfaceBusiness;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Sale = Data.Model.Sale;

namespace BusinessLogic.BusinessLogicLayer;

public class SalesBusinessLayer : ISalesBusinessLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public SalesBusinessLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Registers a sale transaction consisting of multiple products.
    /// 
    /// The method receives a list of product IDs (possibly with duplicates),
    /// groups them to determine quantity per product, and processes each item.
    /// 
    /// For each product:
    /// - Validates stock availability
    /// - Updates stock or volume depending on product type
    /// - Creates one or more Sale records
    /// 
    /// All Sale entities are saved in a single transaction.
    /// </summary>
    /// <param name="productIds">
    /// A list of product IDs representing items in the order.
    /// Duplicate IDs indicate multiple quantities of the same product.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a product is not found or stock is insufficient.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if quantity is invalid (≤ 0).
    /// </exception>
    public async Task RegisterSaleAsync(List<int> productIds)
    {
        var grouped = productIds
            .GroupBy(id => id)
            .Select(g => new { productId = g.Key, quantity = g.Count() });

        var transactionId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        var allSales = new List<Sale>();

        foreach (var item in grouped)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.productId);
            if (product == null)
                throw new InvalidOperationException("Product not found");

            switch (product)
            {
                case Liquid liquid:
                    HandleLiquidSale(liquid, item.quantity);
                    break;

                case Snack snack:
                    HandleSnackSale(snack, item.quantity);
                    break;

                default:
                    throw new NotSupportedException($"Unknown product type {product.GetType().Name}");
            }

            var sales = CreateSales(product, item.quantity, transactionId, now);
            allSales.AddRange(sales);
        }

        await _unitOfWork.Sales.AddRangeAsync(allSales);
        await _unitOfWork.SaveChangesAsync();
    }


    private void HandleSnackSale(Snack snack, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        if (snack.StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock");

        snack.StockQuantity -= quantity;
    }

    /// <summary>
    /// Handles stock reduction for liquid products.
    /// </summary>
    private void HandleLiquidSale(Liquid liquid, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        if (liquid.AlcoholPercentage >= 16)
        {
            if (liquid.StockQuantity == 0)
                throw new InvalidOperationException("Not enough stock");
        }
        else
        {
            if (liquid.StockQuantity < quantity)
                throw new InvalidOperationException("Not enough stock");

            liquid.StockQuantity -= quantity;
        }
    }

    /// <summary>
    /// Creates a list of Sale entities for a given product and quantity.
    /// Each unit sold results in a separate Sale record, all sharing the same transaction ID.
    /// </summary>
    private List<Sale> CreateSales(Product product, int quantity, Guid transactionId, DateTime now)
    {
        var sales = new List<Sale>();

        for (int i = 0; i < quantity; i++)
        {
            var sale = new Sale(product.CostPrice, now, transactionId, product);
            sales.Add(sale);
        }

        return sales;
    }

    /// <summary>
    /// Adds a liquid ingredient to the drink associated with a sale.
    /// </summary>
    public async Task AddIngredient(int saleId, int liquidId)
    {
        var drink = await GetDrinkOrThrow(saleId);
        var liquid = await GetLiquidOrThrow(liquidId);

        var existing = drink.Ingredients.FirstOrDefault(l => l.Id == liquidId);
        if (existing != null)
            throw new InvalidOperationException("Ingredient already exists");

        drink.Ingredients.Add(liquid);

        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Removes a liquid ingredient from the drink associated with a sale.
    /// </summary>
    public async Task RemoveIngredient(int saleId, int liquidId)
    {
        var drink = await GetDrinkOrThrow(saleId);

        var ingredientToRemove = GetIngredientOrThrow(drink, liquidId);

        drink.Ingredients.Remove(ingredientToRemove);

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<Drink> GetDrinkOrThrow(int saleId)
    {
        if (saleId <= 0)
            throw new ArgumentOutOfRangeException(nameof(saleId));

        var sale = await _unitOfWork.Sales.GetAsync(saleId);
        if (sale == null)
            throw new InvalidOperationException("Sale not found");

        if (sale.Drink == null)
            throw new InvalidOperationException("Drink not found");

        return sale.Drink;
    }

    private async Task<Liquid> GetLiquidOrThrow(int liquidId)
    {
        if (liquidId <= 0)
            throw new ArgumentOutOfRangeException(nameof(liquidId));

        var product = await _unitOfWork.Products.GetByIdAsync(liquidId);
        if (product == null)
            throw new InvalidOperationException("Liquid not found");

        if (product is not Liquid liquid)
            throw new InvalidOperationException("Product is not a liquid");

        return liquid;
    }

    private Liquid GetIngredientOrThrow(Drink drink, int liquidId)
    {
        var ingredient = drink.Ingredients.FirstOrDefault(l => l.Id == liquidId);
        if (ingredient == null)
            throw new InvalidOperationException("Ingredient not found");

        return ingredient;
    }
}
