using BusinessLogic.InterfaceBusiness;
using Data.Model;
using Data.UnitOfWork;
using BusinessLogic.Mappers;
using DataTransferObject.Model;
using System.Diagnostics;
using Sale = Data.Model.Sale;

namespace BusinessLogic.BusinessLogicLayer;

public class SalesBusinessLayer : ISalesBusinessLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public SalesBusinessLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterSaleAsync(List<int> productIds, List<int> drinkIds)
    {
        var transactionId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        var allSales = new List<Sale>();

        var productGroups = productIds.GroupBy(id => id).ToDictionary(g => g.Key, g => g.Count());
        var drinkGroups = drinkIds.GroupBy(id => id).ToDictionary(g => g.Key, g => g.Count());

        // -------------------------
        // HANDLE PRODUCT SALES
        // -------------------------
        if (productGroups.Any())
        {
            var products = await _unitOfWork.Products.GetWhereAsync(p => productGroups.Keys.Contains(p.Id));

            foreach (var product in products)
            {
                int qty = productGroups[product.Id];

                if (product is Liquid liquid)
                {
                    HandleLiquidSale(liquid, qty);
                    RegistrerPantFraSalg(liquid, qty);
                }
                else if (product is Snack snack)
                    HandleSnackSale(snack, qty);
                else
                    throw new NotSupportedException($"Unknown product type {product.GetType().Name}");

                allSales.AddRange(CreateSales(product, qty, transactionId, now));
            }
        }

        // -------------------------
        // HANDLE DRINK SALES
        // -------------------------
        if (drinkGroups.Any())
        {
            var drinks = await _unitOfWork.Drinks.GetDrinksWithIngredientsAsync(drinkGroups.Keys.ToList());

            foreach (var drink in drinks)
            {
                int drinkQty = drinkGroups[drink.Id];

                // Reduce stock for each liquid ingredient
                foreach (var liquid in drink.Ingredients)
                {
                    
                    if (liquid.StockQuantity < drinkQty)
                        throw new InvalidOperationException($"Not enough stock for ingredient {liquid.Name}");

                    RegistrerPantFraSalg(liquid, drinkQty);
                    {
                        liquid.StockQuantity -= drinkQty;
                    }

                    allSales.AddRange(CreateSalesForDrink(drink, drinkQty, transactionId, now));
                }
            }

            await _unitOfWork.Sales.AddRangeAsync(allSales);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    private void HandleSnackSale(Snack snack, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        if (snack.StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock");

        snack.StockQuantity -= quantity;
    }

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

    private List<Sale> CreateSales(Product product, int quantity, Guid transactionId, DateTime now)
    {
        var sales = new List<Sale>();

        for (int i = 0; i < quantity; i++)
            sales.Add(new Sale(now, transactionId, product));

        return sales;
    }


    private List<Sale> CreateSalesForDrink(Drink drink, int quantity, Guid transactionId, DateTime now)
    {
        var sales = new List<Sale>();

        for (int i = 0; i < quantity; i++)
            sales.Add(new Sale(now, transactionId, drink));

        return sales;
    }

    public async Task<List<SaleDataTransferObject>> GetAllSalesAsync()
    {
        var sales = await _unitOfWork.Sales.GetAllWithRelationsAsync();
        return sales.Select(SaleMapper.Map).ToList();
    }

    // ---------------------------------------------------------
    // INGREDIENT MANAGEMENT (NEW MANY-TO-MANY MODEL)
    // ---------------------------------------------------------

    public async Task AddIngredient(int saleId, int liquidId)
    {
        var drink = await GetDrinkOrThrow(saleId);
        var liquid = await GetLiquidOrThrow(liquidId);

        if (drink.Ingredients.Any(l => l.Id == liquidId))
            throw new InvalidOperationException("Ingredient already exists");

        drink.Ingredients.Add(liquid);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveIngredient(int saleId, int liquidId)
    {
        var drink = await GetDrinkOrThrow(saleId);

        var ingredient = drink.Ingredients.FirstOrDefault(l => l.Id == liquidId);
        if (ingredient == null)
            throw new InvalidOperationException("Ingredient not found");

        drink.Ingredients.Remove(ingredient);

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
            throw new InvalidOperationException("Sale does not contain a drink");

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


    private void RegistrerPantFraSalg(Liquid liquid, int antal)
    {
        
        var pantType = (PantDataTransferObject)liquid.Pant;

        if (pantType == PantDataTransferObject.None) return;

        var kasseØlSøgeord = new List<string> { "Ceres Top", "Albani øl", "Classic", "Royal" };

        bool erKasseØl = kasseØlSøgeord.Any(ord =>
            liquid.Name.Contains(ord, StringComparison.OrdinalIgnoreCase));

        if (erKasseØl)
        {
            
            PantOptæller.Instance.TilføjØlMedKasseLogik(pantType, antal);
            Debug.WriteLine("Det øl " + PantOptæller.Instance.HentTotalPantVærdi());
        }
        else
        {
            PantOptæller.Instance.TilføjPant(pantType, antal);
            Debug.WriteLine("Det alt andet " + PantOptæller.Instance.HentTotalPantVærdi());

        }
    }


    private decimal getAntalPant()
    {
        return PantOptæller.Instance.HentTotalPantVærdi();

    }
}
