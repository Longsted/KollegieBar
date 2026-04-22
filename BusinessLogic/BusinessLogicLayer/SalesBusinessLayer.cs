using BusinessLogic.InterfaceBusiness;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using Microsoft.VisualBasic.CompilerServices;
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
        var grouped = productIds.GroupBy(id => id).Select(g => new
        {
            productId = g.Key,
            quantity = g.Count()
        });

        var transactionId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        var allSales = new List<Sale>();
        foreach (var item in grouped)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            
            if (product.GetType() == typeof(Drink))
            {
                //TODO handle drink sale here
            }
            else
            {
                switch (product)
                {
                    case Data.Model.Snack snack:
                        HandleSnackSale(snack, item.quantity);
                        break;
                    case Data.Model.Liquid liquid:
                        HandleLiquidSale(liquid, item.quantity);
                        break;

                    default:

                        throw new NotSupportedException($"unknown type product {product.GetType().Name}");
                }
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

    /// <summary>
    /// Handles volume reduction for liquid (alcohol) products.
    /// 
    /// Calculates how many centiliters to remove based on quantity sold.
    /// Assumes each sale corresponds to 20 cl.
    /// </summary>
    /// <param name="liquid">The liquid product to update.</param>
    /// <param name="quantity">The quantity sold.</param>
    /// <exception cref="ArgumentException">If quantity is invalid.</exception>
    /// <exception cref="InvalidOperationException">If there is not enough volume available.</exception>
    private void HandleLiquidSale(Data.Model.Liquid liquid, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Invalid quantity");
        }

        if (liquid.AlcoholPercentage >= 16)
        {
            if (liquid.StockQuantity == 0)
            {
                throw new InvalidOperationException("Not enough stock");
            }
        }
        else
        {
            liquid.StockQuantity -= quantity;
        }
    }

    /// <summary>
    /// Creates a list of Sale entities for a given product and quantity.
    /// 
    /// Each unit sold results in a separate Sale record, all sharing the same transaction ID.
    /// </summary>
    /// <param name="product">The product being sold.</param>
    /// <param name="quantity">Number of units sold.</param>
    /// <param name="transactionId">Unique identifier for the transaction.</param>
    /// <param name="now">Timestamp of the sale.</param>
    /// <returns>A list of Sale entities.</returns>
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

    public async Task AddIngredient(int saleid, DrinkIngredient ingredient)
    {
        if (ingredient == null)
            throw new ArgumentNullException(nameof(ingredient));


        var drink = await GetDrinkOrThrow(saleid);

        var existing = drink.Ingredients
            .FirstOrDefault(i => i.LiquidProductId == ingredient.LiquidProductId);
        
            drink.Ingredients.Add(ingredient);
        

        await _unitOfWork.SaveChangesAsync();
    }

    // public async Task AdjustIngredientAmount(int saleid, int ingredientId, int amount)
    // {
    //     var drink = await GetDrinkOrThrow(saleid);
    //
    //     var existing = GetIngredientOrThrow(drink, ingredientId);
    //
    //     existing.VolumeCl += amount;
    //
    //     if (existing.VolumeCl <= 0)
    //     {
    //         drink.Ingredients.Remove(existing);
    //     }
    //
    //     await _unitOfWork.SaveChangesAsync();
    // }

    public async Task RemoveIngredient(int saleid, int ingredientId)
    {
        var drink = await GetDrinkOrThrow(saleid);

        var ingredientToRemove = GetIngredientOrThrow(drink, ingredientId);

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

    private DrinkIngredient GetIngredientOrThrow(Drink drink, int ingredientId)
    {
        var ingredient = drink.Ingredients
            .FirstOrDefault(i => i.Id == ingredientId);


        if (ingredient == null)
            throw new InvalidOperationException("Ingredient not found");

        return ingredient;
    }
}