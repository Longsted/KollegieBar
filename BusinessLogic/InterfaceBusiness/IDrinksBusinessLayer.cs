using DataTransferObject.Model;

namespace BusinessLogic.InterfaceBusiness;

public interface IDrinksBusinessLogicLayer
{
    Task CreateDrinkAsync(DrinkDataTransferObject drink);

    Task UpdateDrinkAsync(DrinkDataTransferObject drink);

    Task DeleteDrinkAsync(int id);

    Task<DrinkDataTransferObject?> GetDrinkAsync(int id);

    Task<List<DrinkDataTransferObject>> GetAllDrinksAsync();

    Task AddDrinkIngredient(int drinkId, int liquidProductId);

    Task RemoveDrinkIngredient(int drinkId, int ingredientId);
}