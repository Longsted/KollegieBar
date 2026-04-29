using BusinessLogic.InterfaceBusiness;
using BusinessLogic.Mappers;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;

namespace BusinessLogic.BusinessLogicLayer;

public class DrinksBusinessLayer : IDrinksBusinessLogicLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public DrinksBusinessLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task DeleteDrinkAsync(int id)
    {
        var drink = await GetDrinkOrThrow(id);

        await _unitOfWork.Drinks.DeleteAsync(drink);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<DrinkDataTransferObject?> GetDrinkAsync(int id)
    {
        var drink = await GetDrinkOrThrow(id);
        
        return DrinkMapper.Map(drink);
    }

    public async Task<List<DrinkDataTransferObject>> GetAllDrinksAsync()
    {
        var drinks = await _unitOfWork.Drinks.GetAllAsync();
        return drinks.Select(DrinkMapper.Map).ToList();
    }

    public async Task CreateDrinkAsync(DrinkDataTransferObject drink)
    {
        ValidateDrink(drink);

        // Extract unique liquid IDs
        var liquidIds = drink.Ingredients.Select(index => index.Id).Distinct().ToList();

        // Fetch all required liquids
        var liquids = await _unitOfWork.Products.GetWhereAsync(product => liquidIds.Contains(product.Id));

        if (liquids.Count != liquidIds.Count)
            throw new InvalidOperationException("One or more ingredients do not exist");

        if (liquids.Any(product => product is not Liquid))
            throw new InvalidOperationException("All ingredients must be liquids");

        var drinkEntity = DrinkMapper.Map(drink);

        // Replace DTO liquids with tracked EF liquids
        drinkEntity.Ingredients = liquids.Cast<Liquid>().ToList();

        await _unitOfWork.Drinks.AddAsync(drinkEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateDrinkAsync(DrinkDataTransferObject drink)
    {
        ValidateDrinkUpdate(drink);

        var existingDrink = await _unitOfWork.Drinks.GetByIdAsync(drink.Id);
        if (existingDrink == null)
            throw new InvalidOperationException($"There is no drink with id: {drink.Id}");

        // Ingredients are handled separately
        DrinkMapper.MapToEntity(drink, existingDrink);

        await _unitOfWork.SaveChangesAsync();
    }

    // ------------------------------------------------------------
    // INGREDIENT MANAGEMENT
    // ------------------------------------------------------------

    public async Task AddDrinkIngredient(int drinkId, int liquidId)
    {
        var drink = await GetDrinkOrThrow(drinkId);
        var liquid = await GetLiquidOrThrow(liquidId);

        if (drink.Ingredients.Any(l => liquid.Id == liquidId))
            throw new InvalidOperationException("Ingredient already in drink");

        drink.Ingredients.Add(liquid);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveDrinkIngredient(int drinkId, int liquidId)
    {
        var drink = await GetDrinkOrThrow(drinkId);

        var ingredient = drink.Ingredients.FirstOrDefault(l => l.Id == liquidId);
        if (ingredient == null)
            throw new InvalidOperationException("Ingredient not found");

        if (drink.Ingredients.Count == 1)
            throw new InvalidOperationException("A drink must contain at least one ingredient");

        drink.Ingredients.Remove(ingredient);

        await _unitOfWork.SaveChangesAsync();
    }

    // ------------------------------------------------------------
    // -------------------HELPER METHODS---------------------------
    // ------------------------------------------------------------

    private async Task<Drink> GetDrinkOrThrow(int drinkId)
    {
        if (drinkId <= 0)
            throw new ArgumentException("Invalid drink id");

        var drink = await _unitOfWork.Drinks.GetByIdAsync(drinkId);
        if (drink == null)
            throw new InvalidOperationException("Drink not found");

        return drink;
    }

    private async Task<Liquid> GetLiquidOrThrow(int liquidId)
    {
        if (liquidId <= 0)
            throw new ArgumentException("Invalid liquid id");

        var product = await _unitOfWork.Products.GetByIdAsync(liquidId);
        if (product == null)
            throw new InvalidOperationException("Liquid not found");

        if (product is not Liquid liquid)
            throw new InvalidOperationException("Ingredient must be a liquid");

        return liquid;
    }

    private void ValidateDrinkUpdate(DrinkDataTransferObject drink)
    {
        if (drink == null)
            throw new ArgumentNullException(nameof(drink));

        if (drink.Id <= 0)
            throw new ArgumentException("Invalid id");

        if (string.IsNullOrWhiteSpace(drink.Name))
            throw new ArgumentException("Drink name is required");
    }

    private void ValidateDrink(DrinkDataTransferObject drink)
    {
        if (drink == null)
            throw new ArgumentNullException(nameof(drink));

        if (string.IsNullOrWhiteSpace(drink.Name))
            throw new ArgumentException("Drink name is required");

        if (drink.Ingredients == null || !drink.Ingredients.Any())
            throw new ArgumentException("A drink must contain at least one ingredient");

        foreach (var ingredient in drink.Ingredients)
        {
            if (ingredient.Id <= 0)
                throw new ArgumentException("Invalid liquid id");
        }

        bool duplicates = drink.Ingredients
            .GroupBy(index => index.Id)
            .Any(group => group.Count() > 1);

        if (duplicates)
            throw new ArgumentException("The same liquid cannot be added twice");
    }

    public async Task<List<Drink>> GetDrinksWithIngredientsAsync(List<int> drinkIds)
    {
        if (drinkIds == null || !drinkIds.Any())
            return new List<Drink>();

        return await _unitOfWork.Drinks.GetDrinksWithIngredientsAsync(drinkIds);
    }
}
