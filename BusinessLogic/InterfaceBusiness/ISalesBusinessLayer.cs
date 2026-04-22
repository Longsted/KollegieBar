using Data.Model;
namespace BusinessLogic.InterfaceBusiness;

public interface ISalesBusinessLayer
{
    Task RegisterSaleAsync(List<int> productIds);

    Task AddIngredient(int saleid, DrinkIngredient ingredient);
    
    // Task AdjustIngredientAmount(int saleid, int ingredientId, int amount);
    
    Task RemoveIngredient(int saleid, int ingredientId);

   
}