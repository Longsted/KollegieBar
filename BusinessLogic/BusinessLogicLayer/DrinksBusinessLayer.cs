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
        if (id <= 0)
            throw new ArgumentException("Invalid id");
        var drink = await _unitOfWork.Drinks.GetByIdAsync(id);
        if (drink == null)
            throw new InvalidOperationException($"There is no drink with id: {id}");

        await _unitOfWork.Drinks.DeleteAsync(drink);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<DrinkDataTransferObject?> GetDrinkAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id");
        var drink = await _unitOfWork.Drinks.GetByIdAsync(id);
        if (drink == null)
            throw new InvalidOperationException($"There is no drink with id: {id}");

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

        var drinkIngredientId = drink.Ingredients.Select(index => index.LiquidProductId).Distinct().ToList();


        var liquids = await _unitOfWork.Products.GetWhereAsync(products => drinkIngredientId.Contains(products.Id));

        if (liquids.Count != drinkIngredientId.Count)
            throw new InvalidOperationException("One or more ingredients do not exist");

        if (liquids.Any(product => product is not Liquid))
            throw new InvalidOperationException("All ingredients must be a liquid");

        var drinkEntity = DrinkMapper.Map(drink);
        await _unitOfWork.Drinks.AddAsync(drinkEntity);
        await _unitOfWork.SaveChangesAsync();
    }

 
    public async Task UpdateDrinkAsync(DrinkDataTransferObject drink)
    {
        ValidateDrinkUpdate(drink);

        if (drink.Ingredients != null &&
            drink.Ingredients.Any())
        {
            throw new InvalidOperationException("Handle drink ingredients through add/remove methods");
        }


        var existingDrink = await _unitOfWork.Drinks.GetByIdAsync(drink.Id);

        if (existingDrink == null)
        {
            throw new InvalidOperationException($"There is no drink with id: {drink.Id}");
        }

        DrinkMapper.MapToEntity(drink, existingDrink);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task AddDrinkIngredient(int drinkId, int liquidProductId)
    {
        var drink =
            await GetDrinkOrThrow(drinkId);


        await GetLiquidOrThrow(liquidProductId);

        bool exists = drink.Ingredients
            .Any(i => i.LiquidProductId == liquidProductId);

        if (exists)
            throw new InvalidOperationException(
                "Ingredient already exists"
            );
        drink.Ingredients.Add(new DrinkIngredient(liquidProductId));

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveDrinkIngredient(int drinkId, int ingredientId)
    {
        var drink = await GetDrinkOrThrow(drinkId);


        var ingredient = GetIngredientOrThrow(drink, ingredientId);

        // optional: ensure empty recipe cannot happen
        if (drink.Ingredients.Count == 1)
            throw new InvalidOperationException(
                "A drink must contain at least one ingredient"
            );

        drink.Ingredients.Remove(ingredient);

        await _unitOfWork.SaveChangesAsync();
    }

    //--------------------- helper methods ------------------------
    private async Task<Drink> GetDrinkOrThrow(int drinkId)
    {
        if (drinkId <= 0)
            throw new ArgumentException(
                "Invalid drink id"
            );

        var drink =
            await _unitOfWork.Drinks.GetByIdAsync(
                drinkId
            );

        if (drink == null)
            throw new InvalidOperationException(
                "Drink not found"
            );

        return drink;
    }

    private async Task<Liquid> GetLiquidOrThrow(int liquidProductId)
    {
        if (liquidProductId <= 0)
            throw new ArgumentException(
                "Invalid liquid id"
            );

        var product =
            await _unitOfWork.Products.GetByIdAsync(
                liquidProductId
            );

        if (product == null)
            throw new InvalidOperationException(
                "Liquid not found"
            );

        if (product is not Liquid liquid)
            throw new InvalidOperationException(
                "Ingredient must be a liquid"
            );

        return liquid;
    }

    private DrinkIngredient GetIngredientOrThrow(Drink drink, int ingredientId)
    {
        var ingredient = drink.Ingredients
            .FirstOrDefault(i => i.Id == ingredientId
            );

        if (ingredient == null)
            throw new InvalidOperationException(
                "Ingredient not found"
            );

        return ingredient;
    }

    private void ValidateDrinkUpdate(DrinkDataTransferObject drink)
    {
        if (drink == null)
            throw new ArgumentNullException(nameof(drink));

        if (drink.Id <= 0)
            throw new ArgumentException("Invalid id");

        if (string.IsNullOrWhiteSpace(drink.Name))
            throw new ArgumentException();
    }
    
    private void ValidateDrink(DrinkDataTransferObject drink)
    {
        if (drink == null)
        {
            throw new ArgumentNullException(nameof(drink));
        }

        if (string.IsNullOrWhiteSpace(drink.Name))
        {
            throw new ArgumentException("Drink name is required");
        }
        
        if (drink.Ingredients == null || !drink.Ingredients.Any())
        {
            throw new ArgumentException(
                "A drink must contain at least one ingredient"
            );
        }

        foreach (var ingredient in drink.Ingredients)
        {
            if (ingredient.LiquidProductId <= 0)
            {
                throw new ArgumentException(
                    "Invalid liquid product id"
                );
            }
        }

        // no dublicate ingredients
        bool duplicates = drink.Ingredients
            .GroupBy(i => i.LiquidProductId)
            .Any(g => g.Count() > 1);

        if (duplicates)
        {
            throw new ArgumentException(
                "The same liquid cannot be added twice"
            );
        }
    }

    
    
}